using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Pages.Auth
{
    public class XblLoginModel : PageModel
    {
        private readonly AuthenticationServiceXbl _authServXbl;

        public XblLoginModel(AuthenticationServiceXbl authServXbl)
        {
            _authServXbl = authServXbl;
        }
        public ActionResult OnGet()
        {
            return Redirect(_authServXbl.GenerateAuthorizationUrl());
        }
    }
}
