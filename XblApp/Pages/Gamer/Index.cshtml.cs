using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application;
using XblApp.Application.UseCases;

namespace XblApp.Pages.Gamer
{
    public class IndexModel : PageModel
    {
        private readonly GamerProfileUseCase _getGamerProfileUseCase;

        public GamerDTO? Output { get; set; }

        public IndexModel(GamerProfileUseCase getGamerProfileUseCase)
        {
            _getGamerProfileUseCase = getGamerProfileUseCase;
        }

        public async Task<IActionResult> GetGamerProfile(string gamertag)
        {
            GamerDTO? gamer = await _getGamerProfileUseCase.GetGamerProfileAsync(gamertag);
            Output = gamer;
            return Page();
        }
    }
}
