using Microsoft.AspNetCore.Mvc;
using SampleWebApi._4.ViewModels.Users;

namespace SampleWebApi._3.Services
{
    public interface IMyAuthenticationService
    {
        Task<ResponseViewModel<TokenViewModel>> LoginAsync(LoginViewModel model);
        Task<ResponseViewModel> RegisterAsync([FromBody] RegisterViewModel model);
    }
}