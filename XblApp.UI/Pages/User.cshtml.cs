using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.Database.Models;
using XblApp.DTO;

namespace XblApp.UI.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public GamerDTO? Output { get; set; }
        public ApplicationUser? appUser;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GamerProfileUseCase _gamerProfileUseCase;

        public UserModel(UserManager<ApplicationUser> userManager, GamerProfileUseCase gamerProfileUseCase)
        {
            _userManager = userManager;
            _gamerProfileUseCase = gamerProfileUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            appUser = await _userManager.GetUserAsync(User);

            Domain.Entities.Gamer? gamer = await _gamerProfileUseCase.GetGamerProfileAsync(appUser.UserName);
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
