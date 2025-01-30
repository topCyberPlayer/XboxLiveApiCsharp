using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.DTO;

namespace XblApp.Pages.Gamer
{
    public class IndexModel : PageModel
    {
        private readonly GamerProfileUseCase _gamerProfileUseCase;

        [BindProperty(SupportsGet = true)]
        public GamerDTO? Output { get; set; }

        public IndexModel(GamerProfileUseCase gamerProfileUseCase)
        {
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGet(string gamertag)
        {
            Domain.Entities.Gamer? gamer = await _gamerProfileUseCase.GetGamerProfileAsync(gamertag);
            Output = CastToGamerDTO(gamer);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(long gamerId)
        {

            Domain.Entities.Gamer? gamer = await _gamerProfileUseCase.UpdateProfileAsync(gamerId);
            Output = CastToGamerDTO(gamer);
            return Page();
        }

        private static GamerDTO CastToGamerDTO(Domain.Entities.Gamer? gamer) =>
            new()
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                Bio = gamer.Bio,
                Location = gamer.Location,
                Games = gamer.GameLinks.Select(x => x.GameLink).Count(),
                Achievements = gamer.GameLinks.Sum(x => x.CurrentAchievements)
            };
    }
}
