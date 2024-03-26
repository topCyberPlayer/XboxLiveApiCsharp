using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Game
{
    public class IndexModel : PageModel
    {
        private GameService service;

        public GameViewModel Output { get; private set; }

        public IndexModel(GameService tittleHubService)
        {
            service = tittleHubService;
        }

        public IActionResult OnGet(string game)
        {
            Output = service.GetGame(game);

            return Page();
        }
    }
}
