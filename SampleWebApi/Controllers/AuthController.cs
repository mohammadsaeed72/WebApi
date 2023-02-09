using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi._1.Entities;
using SampleWebApi._3.Services;
using SampleWebApi._4.ViewModels.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            var result = await _authenticationService.LoginAsync(model);
            if (result.Status == ResponseStatus.Success)
            {
                return Ok(result.Data);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var result = await _authenticationService.RegisterAsync(model);
            if (result.Status == ResponseStatus.Success)
                return Ok(new ResponseViewModel { Status = ResponseStatus.Success, Message = "کاربر با موفقیت ایجاد شد" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

    }
}
