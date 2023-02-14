using Microsoft.AspNetCore.Mvc;
using SampleWebApi._4.ViewModels.Users;
using SampleWebApi.Controllers.Base;

namespace SampleWebApi.Controllers
{
    public class UserController : MyBaseController
    {
        public UserController()
        {

        }

        [HttpPost("Address")]
        public async Task<IActionResult> AddAddress(AddAddressViewModel addAddress)
        {

            return Ok();
        }

    }
}
