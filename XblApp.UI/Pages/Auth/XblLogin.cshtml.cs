using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.UseCases;

namespace XblApp.Pages.Auth
{
    public class XblLoginModel : PageModel
    {
        private readonly AuthenticationUseCase _authServXbl;

        public XblLoginModel(AuthenticationUseCase authServXbl)
        {
            _authServXbl = authServXbl;
        }
        public ActionResult OnGet()
        {
            return Redirect(_authServXbl.GenerateAuthorizationUrl());
        }
    }
}