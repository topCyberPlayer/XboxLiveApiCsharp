using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
using XblApp.Domain.DTO;

namespace XblApp.Pages.Gamer
{
    public class AchievementsModel(GamerProfileUseCase gamerProfileUseCase) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerAchievementDTO Output { get; set; }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            IEnumerable<GamerAchievementDTO> result = await gamerProfileUseCase.GetGamerAchievementsAsync(gamertag);
            return Page();
        }
    }
}
