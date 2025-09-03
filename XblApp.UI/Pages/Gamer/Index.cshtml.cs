using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.DTO;

namespace XblApp.UI.Pages.Gamer
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
            Output = await _gamerProfileUseCase.GetGamerProfileAsync(gamertag);
            return Page();
        }


    }
}
