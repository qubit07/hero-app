using IcqApp.Entities;

namespace IcqApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
