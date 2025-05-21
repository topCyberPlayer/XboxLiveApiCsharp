using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
using XblApp.DTO;

namespace XblApp.Pages.Game
{
    public class IndexModel : PageModel
    {
        public List<GameDTO> Output { get; set; }

        private readonly GameUseCase _gameUseCase;

        public IndexModel(GameUseCase gameUseCase)
        {
            _gameUseCase = gameUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            List<Domain.Entities.Game> games = await _gameUseCase.GetGamesAsync();
            Output = GameDTO.CastToGameDTO(games).ToList();
            return Page();
        }
    }
}
