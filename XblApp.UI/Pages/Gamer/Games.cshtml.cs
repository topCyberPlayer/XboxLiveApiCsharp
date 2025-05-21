using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.XboxLiveUseCases;
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
            Domain.Entities.Gamer gamerGame = await _gamerProfileUseCase.GetGamesForGamerRepoAsync(gamertag);
            Output = GamerGameDTO.CastToGamerGameDTO(gamerGame);
            return Page();
        }
    }
}
