using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Shared.DTOs;

namespace XblApp.Pages.Gamer
{
    public class AchievementsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerGameAchievementDTO Output { get; set; }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            Output = new GamerGameAchievementDTO 
            { 
                GamerId = 111, 
                Gamertag = gamertag,
                GameAchievements = new List<GameAchievementDTO2>
                {
                    new GameAchievementDTO2()
                    {
                        GameId = 1,
                        GameName = "sadsad",
                        Achievements = new List<GamerAchievementInnerDTO>()
                        {
                            new GamerAchievementInnerDTO()
                            {
                                Score = 10,
                                Name = "Boom",
                                Description = "Desc",
                                IsUnlocked = true,
                            }
                        }
                    }
                }
            };
            return Page();
        }
    }
}
