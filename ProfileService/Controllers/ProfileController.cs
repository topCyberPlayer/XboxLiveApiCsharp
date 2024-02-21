using DomainModel.Profiles;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Models;
using ProfileService.Services;

namespace ProfileService.Controllers
{
    public class ProfileController : ControllerBase
    {
        private ProfileXblService _profileService;

        public ProfileController(ProfileXblService profileService)
        {
            _profileService = profileService;
        }

        public async Task<ActionResult<ProfileXblModel>> GetProfileByXuid(string xuid)
        {
            //ActionResult<ProfileXblModel> profile = await _profileService.GetProfileByXuid(xuid);

            //return profile == null ? NotFound() : Ok(profile);
            return Ok();
        }

        public async Task<ActionResult<ProfileXblModel>> GetProfileByGamertag(string gamertag)
        {
            //ActionResult<ProfileXblModel> profile = await _profileService.GetProfileByGamertag(gamertag);

            //return profile == null ? NotFound() : Ok(profile);
            return Ok();
        }
    }
}
