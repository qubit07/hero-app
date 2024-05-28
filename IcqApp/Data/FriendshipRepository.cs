using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Extensions;
using IcqApp.Helpers;
using IcqApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IcqApp.Data
{
    public class FriendshipRepository : IFriendshipRepository
    {

        private readonly DataContext _dataContext;

        public FriendshipRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<UserFriendship> GetFriendship(int sourceUserId, int targetUserId)
        {
            return await _dataContext.Friendships.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<FriendshipDto>> GetUserFriendShips(FriendshipParams friendshipParams)
        {
            var users = _dataContext.Users.OrderBy(u => u.UserName).AsQueryable();
            var friendships = _dataContext.Friendships.AsQueryable();

            if (friendshipParams.Predicate == "friend")
            {
                friendships = friendships.Where(s => s.SourceUserId == friendshipParams.UserId);
                users = friendships.Select(s => s.TargetUser);
            }
            if (friendshipParams.Predicate == "friendBy")
            {
                friendships = friendships.Where(s => s.TargetUserId == friendshipParams.UserId);
                users = friendships.Select(s => s.SourceUser);
            }
            var friendUsers = users.Select(user => new FriendshipDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url
            });

            return await PagedList<FriendshipDto>.CreateAsync(friendUsers, friendshipParams.PageNumber, friendshipParams.PageSize);
        }

        public async Task<AppUser> GetUserWithFriends(int userId)
        {
            return await _dataContext.Users
                 .Include(x => x.FriendUsers)
                 .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
