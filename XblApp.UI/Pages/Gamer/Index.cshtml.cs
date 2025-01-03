using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.UseCases;
using XblApp.Shared.DTOs;

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
            GamerDTO? gamer = await _gamerProfileUseCase.GetGamerProfileAsync(gamertag);
            Output = gamer;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync(string gamertag)
        {
            GamerDTO? gamer = await _gamerProfileUseCase.UpdateProfileAsync(gamertag);
            Output = gamer;
            return Page();
        }
    }
}
