using Application.XboxLiveUseCases;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XblApp.UI.Pages.Game
{
    public class AchievementsModel(GameUseCase gameUseCase) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public required AchievementDTO Output { get; set; }

        public async Task<IActionResult> OnGet(long gameId)
        {
            Output = await gameUseCase.GetGameWithAchievementsAsync(gameId);
            return Page();
        }
    }
}
