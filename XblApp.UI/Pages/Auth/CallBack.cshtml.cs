using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;

namespace XblApp.Pages.Auth
{
    public class CallBackModel : PageModel
    {
        private readonly AuthenticationUseCase _authServ;

        public CallBackModel(AuthenticationUseCase authServ)
        {
            _authServ = authServ;
        }

        public async Task<IActionResult> OnGet(string code)
        {
            var error = HttpContext.Request.Query["error"];
            var errorDescription = HttpContext.Request.Query["error_description"];

            if (code == null)
                return NotFound(error);

            try
            {
                await _authServ.RequestTokens(code);

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
