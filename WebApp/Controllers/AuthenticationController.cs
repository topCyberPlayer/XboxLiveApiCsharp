using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationServiceDb _authDb;

        public AuthenticationController(AuthenticationServiceDb authDb)
        {
            _authDb = authDb;
        }

        [HttpGet]
        public IActionResult GetAuthorizationHeaderValue()
        {
            if(User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string result = _authDb.GetAuthorizationHeaderValue(userId);

                return result != null ? Ok(result) : NotFound();
            }

            return Unauthorized();
        }
    }
}
