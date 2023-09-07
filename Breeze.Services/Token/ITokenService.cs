using Breeze.Models.Entities;

namespace Breeze.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);
    }
}
