using Microsoft.AspNetCore.Mvc;

namespace TittleHubService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TittleHubController : ControllerBase
    {
        private readonly List<string> _fruit = new List<string> { "Pear", "Lemon", "Peach" };

        public TittleHubController()
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> GetTitleHistory(string xuid, int maxItems = 5)
        {
            string authorizationHeader = Request.Headers.Authorization;

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return BadRequest("Authorization headers are required.");
            }

            return _fruit;
        }
    }
}
