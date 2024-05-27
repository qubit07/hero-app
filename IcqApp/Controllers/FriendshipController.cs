using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Extensions;
using IcqApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IcqApp.Controllers
{
    public class FriendshipController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        public FriendshipController(IUserRepository userRepository, IFriendshipRepository friendshipRepository)
        {
            _userRepository = userRepository;
            _friendshipRepository = friendshipRepository;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddFriendship(string username)
        {
            var sourceUserId = User.GetUserId();
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _friendshipRepository.GetUserWithFriends(sourceUserId);

            if (user == null)
            {
                return NotFound();
            }
            if (sourceUser.UserName == username)
            {
                return BadRequest("Cant add yourself");
            }

            var userFriedship = await _friendshipRepository.GetFriendship(sourceUserId, user.Id);

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

            if (await _userRepository.SaveAllAsync())
            {
                return Ok();
            }

            return BadRequest("Failed to add friendship");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendshipDto>>> GetFriendships(string predicate)
        {
            var users = await _friendshipRepository.GetUserFriendShips(predicate, User.GetUserId());

            return Ok(users);
        }
    }
}
