using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleWebApi._1.Entities;
using SampleWebApi._4.ViewModels.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleWebApi._3.Services
{
    public class MyAuthenticationService : IMyAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public MyAuthenticationService(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        public async Task<ResponseViewModel<TokenViewModel>> LoginAsync(LoginViewModel model)
        {
            ResponseViewModel<TokenViewModel> returnVal = new();


            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var data = new TokenViewModel
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo
                };
                returnVal.Status = ResponseStatus.Success;
                returnVal.Data = data;
            }
            else
            {
                returnVal.Status = ResponseStatus.ValidationError;
                returnVal.Data = null;
                returnVal.Message = "نام کاربری یا رمز عبور اشتباه است";
            }
            return returnVal;
        }

        public async Task<ResponseViewModel> RegisterAsync([FromBody] RegisterViewModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new ResponseViewModel { Status = ResponseStatus.ValidationError, Message = "نام کاربری تکراری است" };

            AppUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new ResponseViewModel { Status = ResponseStatus.Error, Message = "خطا در ایجاد کاربر" };

            return new ResponseViewModel { Status = ResponseStatus.Success, Message = "کاربر با موفقیت ایجاد شد" };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
