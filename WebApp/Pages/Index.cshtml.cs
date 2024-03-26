using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GamerService _profileService;

        public IndexModel(GamerService profileService)
        {
            _profileService = profileService;
        }

        [BindProperty]
        public BindingModel Input { get; set; }
        public List<GamerViewModel> Outputs { get; private set; }
        public GamerViewModel Output { get; private set; }

        public void OnGet()
        {
            Outputs = _profileService.GetProfiles();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                GamerViewModel results = _profileService.GetProfileByGamertag(Input.SearchTerm);

                //return results.Item1 is not null ? Results = results.Item1 : NotFound(results.Item2.Content);
                Output = results;
                return Page();
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