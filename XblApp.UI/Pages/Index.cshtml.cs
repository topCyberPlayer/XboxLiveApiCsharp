using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.UseCases;
using XblApp.Shared.DTOs;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<GamerDTO> GamerOutput { get; private set; }
        public List<GameDTO> GameOutput { get; private set; }

        private readonly GamerProfileUseCase _getGamerProfileUseCase;
        private readonly GameUseCase _getGameUseCase;

        public IndexModel(GamerProfileUseCase getGamerProfileUseCase, GameUseCase gameUseCase)
        {
            _getGamerProfileUseCase = getGamerProfileUseCase;
            _getGameUseCase = gameUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            GamerOutput = await _getGamerProfileUseCase.GetAllGamerProfilesAsync();
            GameOutput = await _getGameUseCase.GetAllGamesAsync();
            return Page();
        }
    }
}
