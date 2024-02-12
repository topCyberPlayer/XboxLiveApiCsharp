using Microsoft.AspNetCore.Mvc;
using ProfileService.Models;
using ProfileService.Services;

namespace ProfileService.Controllers
{
    public class ProfileController : ControllerBase
    {
        private ProfileLowLvl _profile;

        public ProfileController(ProfileLowLvl profile)
        {
            _profile = profile;
        }

        public async Task<ActionResult<ProfileResponse>> GetProfileByXuid(string xuid)
        {
            ActionResult<ProfileResponse> profile = await _profile.GetProfileByXuid(xuid);

            //return profile;// == null ? NotFound() : Ok(profile);
            return BadRequest(profile);
        }

        public async Task<ProfileResponse> GetProfileByGamertag(string gamertag)
        {
            string baseAddress = _profile.PROFILE_URL + $"/users/gt({gamertag})/profile/settings";

            return await _profile.GetProfileBase(baseAddress);
        }
    }
}
