using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Extensions;
using IcqApp.Helpers;
using IcqApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IcqApp.Controllers
{
    public class FriendshipController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;

        public FriendshipController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddFriendship(string username)
        {
            var sourceUserId = User.GetUserId();
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.FriendshipRepository.GetUserWithFriends(sourceUserId);

            if (user == null)
            {
                return NotFound();
            }
            if (sourceUser.UserName == username)
            {
                return BadRequest("Cant add yourself");
            }

            var userFriedship = await _unitOfWork.FriendshipRepository.GetFriendship(sourceUserId, user.Id);

            if (userFriedship != null)
            {
                return BadRequest("Already added");
            }

            userFriedship = new UserFriendship
            {
                SourceUserId = sourceUserId,
                TargetUserId = user.Id,
            };

            sourceUser.FriendUsers.Add(userFriedship);

            if (await _unitOfWork.Complete())
            {
                return Ok();
            }

            return BadRequest("Failed to add friendship");
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<FriendshipDto>>> GetFriendships([FromQuery] FriendshipParams friendshipParams)
        {
            friendshipParams.UserId = User.GetUserId();
            var users = await _unitOfWork.FriendshipRepository.GetUserFriendShips(friendshipParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            return Ok(users);
        }
    }
}
