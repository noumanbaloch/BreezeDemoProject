using Breeze.Models.Constants;
using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Dtos.User.Response;
using Breeze.Models.GenericResponses;

namespace Breeze.Services.Account
{
    public class AccountFacadeService : IAccountFacadeService
    {
        private readonly IAccountService _accountService;

        public AccountFacadeService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto)
        {
            if (_accountService.UserExist(requestDto.UserName))
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessages.RECORD_ALREADY_EXIST, ApiStatusCodes.RECORD_ALREADY_EXIST);
            }

            var result = await _accountService.RegisterUser(requestDto);

            return GenericResponse<UserResponseDto>.Success(result, ApiResponseMessages.RECORD_SAVED_SUCCESSFULLY, ApiStatusCodes.RECORD_SAVED_SUCCESSFULLY);

        }

        public async Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto)
        {
            var result = await _accountService.LoginUser(requestDto);

            if(result is null)
            {
                return GenericResponse<UserResponseDto>.Failure(ApiResponseMessages.INVALID_USERNAME_OR_PASSWORD, ApiStatusCodes.INVALID_USERNAME_OR_PASSWORD);
            }

            return GenericResponse<UserResponseDto>.Success(result, ApiResponseMessages.SUCCESSFULLY_LOGIN, ApiStatusCodes.SUCCESSFULLY_LOGIN);
        }
    }
}
