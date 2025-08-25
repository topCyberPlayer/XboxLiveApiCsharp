using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.DTO;

namespace XblApp.UI.Pages
{
    public class IndexModel(GamerProfileUseCase getGamerProfileUseCase, GameUseCase gameUseCase) : PageModel
    {
        public IEnumerable<GamerDTO>? GamerOutput { get; private set; }
        public IEnumerable<GameDTO>? GameOutput { get; private set; }

        public async Task<IActionResult> OnGet()
        {
            GamerOutput = await getGamerProfileUseCase.GetAllGamerProfilesAsync();
            GameOutput = await gameUseCase.GetGamesAsync();

            return Page();
        }
    }
}
