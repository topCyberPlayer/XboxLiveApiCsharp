using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XblApp.Application.UseCases;
using XblApp.Shared.DTOs;

namespace XblApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GamerProfileUseCase _getGamerProfileUseCase;
        public List<GamerDTO> Output { get; private set; }

        public IndexModel(GamerProfileUseCase getGamerProfileUseCase)
        {
            _getGamerProfileUseCase = getGamerProfileUseCase;
        }

        public async Task<IActionResult> OnGet()
        {
            Output = await _getGamerProfileUseCase.GetAllGamerProfilesAsync();
            return Page();

            //return RedirectToPage("Auth/XblLogin");            
            //return RedirectToPage("Gamer/Index", new { gamertag = "Riotgran" });
        }

    }
}
