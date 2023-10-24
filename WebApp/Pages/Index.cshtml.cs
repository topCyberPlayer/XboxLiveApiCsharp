using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data.Profile;
using WebApp.Pages.Profiles;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProfileUserProviderDb _profileUserProviderDb;
        public IEnumerable<UserProfilesViewModel> UserProfiles { get; private set; }

        public IndexModel(ProfileUserProviderDb profileUserProviderDb)
        {
            _profileUserProviderDb = profileUserProviderDb;
        }

        public async Task OnGet()
        {
            UserProfiles = await _profileUserProviderDb.GetUserProfiles();
        }
    }
}