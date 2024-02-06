using Microsoft.AspNetCore.Mvc;
using XboxLiveService.Services;

namespace XboxLiveService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationProvider _authPrvdr;

        public AuthenticationController(AuthenticationProvider authPrvdr)
        {
            _authPrvdr = authPrvdr;
        }

        [HttpGet]
        public string Get()
        {
            return _authPrvdr.GenerateAuthorizationUrl();
        }
    }
}
