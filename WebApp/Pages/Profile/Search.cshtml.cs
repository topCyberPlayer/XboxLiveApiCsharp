using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApp.Services;

namespace WebApp.Pages.Profile
{
    public class SearchModel : PageModel
    {
        private readonly AuthenticationService _authService;

        public SearchModel(AuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }

        [BindProperty]
        public BindingModel Input { get; set; }
        public ProfileViewModel Results { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var results = await _authService.GetProfileByGamertag(Input.SearchTerm, userId);

                //return results.Item1 is not null ? Results = results.Item1 : NotFound(results.Item2.Content);
                if (results.Item1 is not null)
                {
                    Results = results.Item1;
                    return Page();
                }
                
            }
            return RedirectToPage();

        }

        public class BindingModel
        {
            [BindProperty]
            [Required]
            [MinLength(3)]
            [Display(Name = "Search T")]
            public string SearchTerm { get; set; }
        }
    }
}
