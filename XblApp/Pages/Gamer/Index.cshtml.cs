using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.GamerServices;
using ServiceLayer.Models;

namespace XblApp.Pages.Gamer
{
    public class IndexModel : PageModel
    {
        private readonly GamerServiceDb _gamerService;

        public GamerModelDto? Output { get; set; }

        public IndexModel(GamerServiceDb gamerService)
        {
            _gamerService = gamerService;
        }

        public IActionResult OnGet(string gamertag)
        {
            Output = _gamerService.FindByGamertag(gamertag).FirstOrDefault();
            return Page();
        }
    }
}
