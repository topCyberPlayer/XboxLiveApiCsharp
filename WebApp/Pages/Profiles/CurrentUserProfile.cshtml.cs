using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profiles
{
    
    public class CurrentUserProfileModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public CurrentUserProfileModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task OnGet()
        {
            //Добавить логику получение данных профиля из БД:ProfileUser

            var asdfdf = await _signInManager.GetExternalAuthenticationSchemesAsync();
            var b = await _signInManager.ExternalLoginSignInAsync("Microsoft", "0f29ee4d4b9131de", true);

            //var authenticateResult = await HttpContext.AuthenticateAsync("Microsoft");
            //var identity = HttpContext.User.Identity;
            //var identities = HttpContext.User.Identities;
            //;

            //if (authenticateResult.Succeeded)
            //{
            //    // Получите `authorization_code` из authenticateResult.Properties
            //    IDictionary<string, string?> items = authenticateResult.Properties.Items;

            //    // Теперь вы можете использовать `authorizationCode` для запроса токена OAuth2.
            //    // Дополнительная логика обработки токена здесь.

            //    var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
            //    var refreshToken = authenticateResult.Properties.GetTokenValue("refresh_token");

            //    // Получите информацию о пользователе
            //    var user = authenticateResult.Principal;
            //}
            //else
            //{
            //    // Обработка неудачной аутентификации
            //}

        }
    }
}
