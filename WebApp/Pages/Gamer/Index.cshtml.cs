using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Gamer
{
    public class IndexModel : PageModel
    {
        private GamerService service;

        public GamerViewModel Output { get; set; }

        public IndexModel(GamerService gamerService)
        {
            service = gamerService;
        }

        public IActionResult OnGet(string gamertag)
        {
            Output = service.GetProfileByGamertag(gamertag);
            return Page();
        }
    }
}
