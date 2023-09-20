using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;

namespace WebApp.Pages.Account
{
    public class MicrosoftLoginModelModel : PageModel
    {
        public string ReturnUrl { get; set; }
        private readonly WebAppContext _context;

        public MicrosoftLoginModelModel(WebAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string returnUrl = "/")
        {
            ReturnUrl = returnUrl;
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("./MicrosoftCallback", pageHandler: null, values: null, protocol: Request.Scheme),
            };

            return Challenge(properties, "Microsoft");
        }
    }
}
