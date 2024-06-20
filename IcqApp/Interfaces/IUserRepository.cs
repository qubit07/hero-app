using IcqApp.DTOs;
using IcqApp.Entities;
using IcqApp.Helpers;

namespace IcqApp.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<PagedList<AppUser>> GetUsersAsync(UserParams userParams);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<MemberDto> GetMemberAsync(string username);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    }
}
