using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MusicQueues.Api.Controllers
{
    [ApiController]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public string GetUserId()
        {
            if (Request.Cookies.ContainsKey("AnonymousUserId"))
            {
                return Request.Cookies["AnonymousUserId"];
            }
            else
            {
                var userId = Guid.NewGuid().ToString();
                Response.Cookies.Append("AnonymousUserId", userId, new CookieOptions());
                return userId;
            }
        }
    }
}