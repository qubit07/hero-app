using AutoMapper;
using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Extensions;
using IcqApp.Helpers;
using IcqApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IcqApp.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoService photoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            userParams.CurrentUsername = currentUser.UserName;

            var users = await _unitOfWork.UserRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(memberUpdateDto, user);

            var isUpdated = await _unitOfWork.Complete();

            if (isUpdated)
            {
                return NoContent();
            }
            return BadRequest("Failed to update user");
        }

        [HttpPost("upload-photo")]
        public async Task<ActionResult<PhotoDto>> UploadPhoto(IFormFile file)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null)
            {
                return NotFound();
            }

            var result = await _photoService.UploadImageAsync(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,

            };

            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if (await _unitOfWork.Complete())
            {
                return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Failed to upload photo");

        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null)
            {
                return NotFound();
            }
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
            if (photo == null)
            {
                return NotFound();
            }

            if (photo.IsMain)
            {
                return BadRequest("Already main photo");
            }

            var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);
            if (currentMain != null)
            {
                currentMain.IsMain = false;
            }
            photo.IsMain = true;
            if (await _unitOfWork.Complete())
            {
                return NoContent();
            }
            return BadRequest("Failed ot set main photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());
            if (user == null)
            {
                return NotFound();
            }
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null)
            {
                return NotFound();
            }
            if (photo.IsMain)
            {
                return BadRequest("Cant delete main foto");
            }
            if (photo.PublicId != null)
            {
                var result = await _photoService.DeleteImageAsync(photo.PublicId);
                if (result.Error != null)
                {
                    return BadRequest(result.Error.Message);
                }
            }

            user.Photos.Remove(photo);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }
            return BadRequest("Failed to delete photo");

        }

    }



}
