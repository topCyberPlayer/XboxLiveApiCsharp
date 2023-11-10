using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services.ProfileUser;

namespace WebApp.Pages.Profiles
{
    [Authorize]
    public class CurrentUserProfileModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public ProfileUserModelDb ProfileUser { get; set; }

        private ProfileUserLogic _profileUserLogic;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CurrentUserProfileModel(ProfileUserLogic profileUserLogic, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _profileUserLogic = profileUserLogic;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                IdentityUser? user = await _userManager.GetUserAsync(User);

                ProfileUser = await _profileUserLogic.GetUser(user.Id);
            }
        }

        public async Task OnPostUpdateProfile()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);

            ProfileUser = await _profileUserLogic.UpdateProfile(user.Id);
        }
    }
}