using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.Pages.Gamer
{
    public class GamesModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerGameDTO Output { get; set; }

        private readonly GamerProfileUseCase _gamerProfileUseCase;

        public GamesModel(GamerProfileUseCase gamerProfileUseCase)
        {
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGetAsync(string gamertag)
        {
            Domain.Entities.Gamer gamerGame = await _gamerProfileUseCase.GetGamesForGamerAsync(gamertag);
            Output = MapToGamerGameDTO(gamerGame);
            return Page();
        }

        private static GamerGameDTO MapToGamerGameDTO(Domain.Entities.Gamer gamer) =>
            new GamerGameDTO()
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Games = gamer.GameLinks.Select(gg => new GameInnerDTO
                {
                    GameId = gg.GameId,
                    GameName = gg.GameLink.GameName,
                    TotalAchievements = gg.GameLink.TotalAchievements,
                    TotalGamerscore = gg.GameLink.TotalGamerscore,
                    CurrentAchievements = gg.CurrentAchievements,
                    CurrentGamerscore = gg.CurrentGamerscore
                }).ToList()
            };
    }
}
