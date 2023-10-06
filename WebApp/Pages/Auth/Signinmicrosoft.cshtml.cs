using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Auth
{
    public class SigninmicrosoftModel : PageModel
    {
        public async Task OnGet()
        {
            var result = await Request.HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);
        }
    }
}
