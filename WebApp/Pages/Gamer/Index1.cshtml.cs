using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Pages.Gamer
{
    public class Index1Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }

        public void OnGet()
        {
            var a = Input.Gamertag;
            var b = Input.Game;
        }
    }

    public class InputModel
    {
        [Required]
        public string Gamertag { get; set; }

        [Required]
        public string Game { get; set; }
    }
}
