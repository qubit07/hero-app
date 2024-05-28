using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Helpers;

namespace IcqApp.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<UserFriendship> GetFriendship(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithFriends(int userId);
        Task<PagedList<FriendshipDto>> GetUserFriendShips(FriendshipParams friendshipParams);
    }
}
