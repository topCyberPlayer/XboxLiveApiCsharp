using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.GamerServices;

namespace XblApp.Pages.Gamer
{
    public class IndexModel : PageModel
    {
        private readonly GamerService _gamerService;

        public GamerListDto Output { get; set; }

        public IndexModel(GamerService gamerService)
        {
            _gamerService = gamerService;
        }

        public IActionResult OnGet(string gamertag)
        {
            Output = _gamerService.FindByGamertag(gamertag);
            return Page();
        }
    }
}
