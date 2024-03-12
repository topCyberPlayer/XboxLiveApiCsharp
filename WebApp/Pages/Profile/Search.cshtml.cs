using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Profile
{
    public class SearchModel : PageModel
    {
        [BindProperty]
        [Required]
        [MinLength(3)]
        [Display(Name ="Search T")]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                //Results = _productService.Search(SearchTerm, StringComparison.Ordinal);
            }

        }
    }
}
