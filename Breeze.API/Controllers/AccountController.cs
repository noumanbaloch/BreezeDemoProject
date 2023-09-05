using Breeze.Models.Dtos.User.Request;
using Breeze.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace Breeze.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserRequestDto requestDto) => 
            Ok(await _accountService.RegisterUser(requestDto));

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequestDto requestDto) => 
            Ok(await _accountService.LoginUser(requestDto));

    }
}
