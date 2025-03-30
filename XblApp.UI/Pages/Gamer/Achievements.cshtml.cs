using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.Pages.Gamer
{
    public class AchievementsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerGameAchievementDTO Output { get; set; }

        private readonly GamerProfileUseCase _gamerProfileUseCase;

        public AchievementsModel(GamerProfileUseCase gamerProfileUseCase)
        {
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            var result = await _gamerProfileUseCase.GetGamerAchievementsAsync(gamertag);
            Output = GamerGameAchievementDTO.CastTo(result);
            return Page();
        }

        //public async Task<IActionResult> OnGetAsync(long gamerId)
        //{
        //    Output = new GamerGameAchievementDTO
        //    {
        //        GamerId = 111,
        //        Gamertag = gamertag,
        //        GameAchievements = new List<GameAchievementDTO2>
        //        {
        //            new GameAchievementDTO2()
        //            {
        //                GameId = 1,
        //                GameName = "Gears",
        //                Achievements = new List<GamerAchievementInnerDTO>()
        //                {
        //                    new GamerAchievementInnerDTO()
        //                    {
        //                        Score = 10,
        //                        Name = "Boom",
        //                        Description = "Kill 10 enemies using Boomshot",
        //                        IsUnlocked = true,
        //                    },
        //                }
        //            },
        //            new GameAchievementDTO2()
        //            {
        //                GameId = 1,
        //                GameName = "Gears",
        //                Achievements = new List<GamerAchievementInnerDTO>()
        //                {
        //                    new GamerAchievementInnerDTO()
        //                    {
        //                        Score = 20,
        //                        Name = "Active Reload",
        //                        Description = "10 Perfect Active Reloads",
        //                        IsUnlocked = true,
        //                    }
        //                }
        //            },
        //            new GameAchievementDTO2()
        //            {
        //                GameId = 2,
        //                GameName = "Battlefiled",
        //                Achievements = new List<GamerAchievementInnerDTO>()
        //                {
        //                    new GamerAchievementInnerDTO()
        //                    {
        //                        Score = 50,
        //                        Name = "Demolition",
        //                        Description = "Destroyed 10 buildings",
        //                        IsUnlocked = true,
        //                    }
        //                }
        //            }
        //        }
        //    };
        //    return Page();
        //}
    }
}
