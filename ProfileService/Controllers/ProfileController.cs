using Microsoft.AspNetCore.Mvc;
using ProfileService.Services;


namespace ProfileService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProfileController : ControllerBase
    {
        private ProfileServiceR _profileService;

        private readonly List<string> _fruit = new List<string> { "Pear", "Lemon", "Peach" };

        public ProfileController(ProfileServiceR profileService)
        {
            _profileService = profileService;
        }

        //public async Task<ActionResult<ProfileXblModel>> GetProfileByXuid(string xuid)
        //{
        //    //ActionResult<ProfileXblModel> profile = await _profileService.GetProfileByXuid(xuid);

        //    //return profile == null ? NotFound() : Ok(profile);
        //    return Ok();
        //}

        [HttpGet]
        //[HttpGet(Name = "GetWeather")] - Это Conventional routing (маршрутизация на основе соглашений)
        public ActionResult<string> GetProfileByGamertag(int id)
        {
            //Query string values: http://localhost:5245/api/Profile/GetProfileByGamertag?id=2
            
            //ActionResult<ProfileModelXbl> profile = await _profileService.GetProfileByGamertag(gamertag);

            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }

        [HttpGet("fruit/{id}")]
        public ActionResult<string> View(int id)
        {
            //Route values (path в Postman): http://localhost:5245/api/Profile/View/fruit/2
            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Vieww(int id)
        {
            //Route values (path в Postman): http://localhost:5245/api/Profile/Vieww/2
            if (id >= 0 && id < _fruit.Count)
            {
                return _fruit[id];
            }

            return NotFound();
        }
    }
}
