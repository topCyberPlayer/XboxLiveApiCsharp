using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Shared.DTOs;

namespace XblApp.Pages.Gamer
{
    public class AchievementsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerAchievementDTO Output { get; set; }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            Output = new GamerAchievementDTO { GamerId = 111, Gamertag = gamertag };//todo исправить
            return Page();
        }
    }
}
