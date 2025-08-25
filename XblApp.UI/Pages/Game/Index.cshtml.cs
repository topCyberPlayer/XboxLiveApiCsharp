using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.DTO;

namespace XblApp.UI.Pages.Game
{
    public class IndexModel(GameUseCase gameUseCase) : PageModel
    {
        public required IEnumerable<GameDTO> Output { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Output = await gameUseCase.GetGamesAsync();
            return Page();
        }
    }
}
