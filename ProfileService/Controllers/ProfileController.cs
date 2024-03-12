using Microsoft.AspNetCore.Mvc;
using ProfileService.Profiles;
using ProfileService.Services;


namespace ProfileService.Controllers
{
    public class ProfileController : ControllerBase
    {
        private Services.ProfileService _profileService;

        public ProfileController(Services.ProfileService profileService)
        {
            _profileService = profileService;
        }

        //public async Task<ActionResult<ProfileXblModel>> GetProfileByXuid(string xuid)
        //{
        //    //ActionResult<ProfileXblModel> profile = await _profileService.GetProfileByXuid(xuid);

        //    //return profile == null ? NotFound() : Ok(profile);
        //    return Ok();
        //}

        public async Task<ActionResult<ProfileModelXbl>> GetProfileByGamertag(string gamertag)
        {
            //ActionResult<ProfileModelXbl> profile = await _profileService.GetProfileByGamertag(gamertag);

            //return profile == null ? NotFound() : Ok(profile);
            return Ok();
        }
    }
}
