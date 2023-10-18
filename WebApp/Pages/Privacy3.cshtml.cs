using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Privacy2Model : PageModel
    {
        private readonly ILogger<Privacy2Model> _logger;

        public Privacy2Model(ILogger<Privacy2Model> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {

        }
    }
}