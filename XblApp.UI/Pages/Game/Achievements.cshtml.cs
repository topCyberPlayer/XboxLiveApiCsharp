using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Domain.DTO;

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
            GameDTO game = await _gameUseCase.GetGameAsync(gameId);
            return Page();
        }
    }
}
