using Microsoft.AspNetCore.Mvc;
using XboxLiveService.Services;

namespace XboxLiveService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationLowerLvl _authLowerLvl;

        public AuthenticationController(AuthenticationLowerLvl authLowerLvl)
        {
            _authLowerLvl = authLowerLvl;
        }

        [HttpGet]
        public string GetAuthorizationUrl()
        {
            return _authLowerLvl.GenerateAuthorizationUrl();
        }
    }
}
