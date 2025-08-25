using Application.XboxLiveUseCases;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XblApp.UI.Pages.Gamer
{
    public class AchievementsModel(GamerProfileUseCase gamerProfileUseCase) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public required GamerAchievementDTO Output { get; set; }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            Output = await gamerProfileUseCase.GetGamerAchievementsAsync(gamertag);
            return Page();
        }
    }
}
