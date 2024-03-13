using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebApp.Services;

namespace WebApp.Pages.Profile
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        [Required]
        [MinLength(3)]
        [Display(Name ="Search T")]
        public string SearchTerm { get; set; }

        private AuthenticationService _authService;


        public SearchModel(AuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }

        public void OnGet()
        {
        }

        public async Task OnPost()
        {
            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                HttpResponseMessage response = await _authService.GetProfile(SearchTerm, userId);

                if (response.IsSuccessStatusCode)
                {

                }
            }

        }
    }
}
