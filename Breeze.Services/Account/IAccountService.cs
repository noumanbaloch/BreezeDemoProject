using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Dtos.User.Response;
using Breeze.Models.GenericResponses;

namespace Breeze.Services.Account
{
    public interface IAccountService
    {
        bool UserExist(string username);
        Task<UserResponseDto> RegisterUser(RegisterUserRequestDto requestDto);
        Task<UserResponseDto?> LoginUser(LoginUserRequestDto requestDto);

    }
}
