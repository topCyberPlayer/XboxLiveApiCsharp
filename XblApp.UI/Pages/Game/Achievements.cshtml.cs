using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
using XblApp.DTO;

namespace XblApp.UI.Pages.Game
{
    public class AchievementsModel : PageModel
    {
        private readonly GameUseCase _gameUseCase;
        
        [BindProperty(SupportsGet = true)]
        public AchievementDTO Output { get; set; }

        public AchievementsModel(GameUseCase gameUseCase)
        {
            _gameUseCase = gameUseCase;
        }

        public async Task<IActionResult> OnGet(long gameId)
        {
            Domain.Entities.Game game = await _gameUseCase.GetGameAsync(gameId);
            Output = AchievementDTO.CastTo(game);
            return Page();
        }
    }
}
