using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Profile;
using WebApp.Services.Authentication;

namespace WebApp.Pages.Profiles
{
    [Authorize]
    public class CurrentUserProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public string ReturnUrl { get; set; }
        private readonly WebAppDbContext _context;
        public ProfileUserModelDb ProfileUserList { get; set; } = default!;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CurrentUserProfileModel(
            WebAppDbContext context, 
            IHttpClientFactory httpClientFactory, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;

            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                IdentityUser user = await _userManager.GetUserAsync(User);

                if (_context.ProfileUsers != null)
                {
                    ProfileUserList = await _context.ProfileUsers.FirstOrDefaultAsync(u => u.AspNetUsersId == user.Id);
                }
            }
        }

        public async Task OnPostUpdateProfile(string returnUrl = null)
        {
            string refreshTokenTable = await GetRefreshTokensAsync();//Считываем таблицу TokenOauth2Table из БД

            AuthenticationLow authLow = new AuthenticationLow(_httpClientFactory);
            TokenOauth2Response tokenOAuth2Response = await authLow.RefreshOauth2Token(refreshTokenTable);//Обновляем TokenOauth2
                                                                                                  // oAuth2TokenResponse сохранить в БД в таблицу TokenOauth2Table
            TokenXauResponse xauResponse = await authLow.RequestXauToken(tokenOAuth2Response);//Обновляем TokenUser
            TokenXstsResponse xstsResponse = await authLow.RequestXstsToken(xauResponse);////Обновляем TokenXsts

            //ProfileUserList = await _context.
        }

        private async ValueTask<string> GetRefreshTokensAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                IdentityUser user = await _userManager.GetUserAsync(User);

                TokenOauth2Table oauthToken = await _context.TokenOauth2s.FirstOrDefaultAsync(a => a.AspNetUserId == user.Id);

                return oauthToken.RefreshToken;
            }

            return string.Empty;
        }
    }
}