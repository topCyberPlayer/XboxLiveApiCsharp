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
            Output = GamerDTO.CastToGamerDTO(gamer);
            return Page();
        }

        
    }
}
