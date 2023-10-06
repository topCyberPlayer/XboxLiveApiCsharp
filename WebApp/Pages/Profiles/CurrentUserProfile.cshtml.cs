using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Profiles
{
    public class CurrentUserProfileModel : PageModel
    {
        public async Task OnGet()
        {
            var asd = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/identityprovider")?.Value;

            //signInManager.

            var props = new AuthenticationProperties();
            //props.StoreTokens(info.AuthenticationTokens);
            //props.IsPersistent = false;

            var authenticateResult = await HttpContext.AuthenticateAsync("Microsoft");

            if (authenticateResult.Succeeded)
            {
                // Получите `authorization_code` из authenticateResult.Properties
                IDictionary<string, string?> items = authenticateResult.Properties.Items;

                // Теперь вы можете использовать `authorizationCode` для запроса токена OAuth2.
                // Дополнительная логика обработки токена здесь.

                var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
                var refreshToken = authenticateResult.Properties.GetTokenValue("refresh_token");

                // Получите информацию о пользователе
                var user = authenticateResult.Principal;
            }
            else
            {
                // Обработка неудачной аутентификации
            }
            
        }
    }
}
