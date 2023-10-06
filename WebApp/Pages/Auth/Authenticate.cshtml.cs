using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Auth
{
    public class AuthenticateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public bool IsAuthenticated => User.Identity.IsAuthenticated;
        public string AuthenticationUrl => Url.Action("Login", "Account", new { area = "Identity" });


        public AuthenticateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public async Task OnGet()
        {
            var result = await Request.HttpContext.AuthenticateAsync(MicrosoftAccountDefaults.AuthenticationScheme);
        }
    }
}
