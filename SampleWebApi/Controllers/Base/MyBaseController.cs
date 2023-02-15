using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi._1.Entities;
using System.Security.Claims;

namespace SampleWebApi.Controllers.Base
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MyBaseController:ControllerBase
    {
        public string UserId = null;
        public MyBaseController()
        {
            UserId = GetUserId(User);
        }
        private string GetUserId(ClaimsPrincipal? principal)
        {
            if (principal == null)
            {
                return null;
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? claim.Value : null;
        }
    }

    
}
