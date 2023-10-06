using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Account
{
    public class MicrosoftCallbackModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            var authenticateResult1 = await HttpContext.AuthenticateAsync();
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

            // Перенаправьте пользователя обратно на главную страницу или другую страницу.
            return RedirectToPage("/Index");
        }
    }
}
