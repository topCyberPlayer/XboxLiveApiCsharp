using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationServiceDb _authDb;

        public AuthenticationController(AuthenticationServiceDb authDb)
        {
            _authDb = authDb;
        }

        //public IActionResult GetAuthorizationHeaderValue()
        //{
        //    //_authDb.GetAuthorizationHeaderValue();
        //    return BadRequest("буу");
        //}
    }
}
