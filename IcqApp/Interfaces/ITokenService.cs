using HeroApp.Entities;

namespace HeroApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
