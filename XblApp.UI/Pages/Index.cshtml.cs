using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.DTO;
using XblApp.Domain.DTO;

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
            IEnumerable<GamerDTO> gamers = await _getGamerProfileUseCase.GetAllGamerProfilesAsync();
            IEnumerable<GameDTO> games = await _getGameUseCase.GetGamesAsync();

            return Page();
        }
    }
}
