using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Dtos.User.Response;
using Breeze.Models.GenericResponses;

namespace Breeze.Services.Account
{
    public interface IAccountService
    {
        Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto);
        Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto);

    }
}
