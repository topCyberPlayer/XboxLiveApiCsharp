using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApp.Pages.Profile;
using WebApp.Services;

namespace WebApp.Pages.TitleHub
{
    public class SearchModel : PageModel
    {
        private readonly TittleHubService _tittleHubService;

        public SearchModel(TittleHubService tittleHubService)
        {
            _tittleHubService = tittleHubService;
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

                var results = await _tittleHubService.GetTitleHistory(Input.SearchTerm, userId);

                //return results.Item1 is not null ? Results = results.Item1 : NotFound(results.Item2.Content);
                Results = results;
                return Page();

            }
            return RedirectToPage();

        }

        public class BindingModel
        {
            [BindProperty]
            [Required]
            [MinLength(3)]
            [Display(Name = "Search a Game")]
            public string SearchTerm { get; set; }
        }
    }
}
