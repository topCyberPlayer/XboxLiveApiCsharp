using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.UseCases;
using XblApp.Shared.DTOs;

namespace XblApp.Pages.Gamer
{
    public class GamesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<GameDTO> Output { get; set; }

        private readonly GamerProfileUseCase _gamerProfileUseCase;        

        public GamesModel(GamerProfileUseCase gamerProfileUseCase)
        {
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            Output = await _gamerProfileUseCase.GetGamesForGamerAsync(gamertag);
            return Page();
        }
    }
}
