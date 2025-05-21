using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
using XblApp.DTO;

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
            List<Domain.Entities.Gamer> gamers = await _getGamerProfileUseCase.GetAllGamerProfilesRepoAsync();
            GamerOutput = GamerDTO.CastToGamerDTO(gamers).ToList();

            List<Domain.Entities.Game> games = await _getGameUseCase.GetGamesAsync();
            GameOutput = GameDTO.CastToGameDTO(games).ToList();

            return Page();
        }
    }
}
