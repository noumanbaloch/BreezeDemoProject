using Breeze.Models.Dtos.User.Request;
using Breeze.Models.Dtos.User.Response;
using Breeze.Models.GenericResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Services.Account
{
    public interface IAccountService
    {
        Task<GenericResponse<UserResponseDto>> RegisterUser(RegisterUserRequestDto requestDto);
        Task<GenericResponse<UserResponseDto>> LoginUser(LoginUserRequestDto requestDto);

    }
}
