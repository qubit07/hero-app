using IcqApp.DTOs;
using IcqApp.Entities;

namespace IcqApp.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<UserFriendship> GetFriendship(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithFriends(int userId);
        Task<IEnumerable<FriendshipDto>> GetUserFriendShips(string predicate, int userId);
    }
}
