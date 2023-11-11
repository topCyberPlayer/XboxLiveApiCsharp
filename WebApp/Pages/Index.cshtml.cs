using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages.Profiles;
using WebApp.Services.ProfileUser;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProfileUserProviderDb _profileUserProviderDb;
        //public IEnumerable<UserProfilesViewModel> ProfileUsers { get; private set; }
        public IEnumerable<ProfileUserModelDb> ProfileUsers { get; private set; }

        public IndexModel(ProfileUserProviderDb profileUserProviderDb)
        {
            _profileUserProviderDb = profileUserProviderDb;
        }

        public async Task OnGet()
        {
            ProfileUsers = await _profileUserProviderDb.GetUserProfiles();
        }
    }
}