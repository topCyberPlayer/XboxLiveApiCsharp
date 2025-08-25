using Application.XboxLiveUseCases;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XblApp.UI.Pages.Admin
{
    [Authorize(Roles = "adminTeam")]
    public class DonorModel(AuthenticationUseCase authUseCase) : PageModel
    {
        public IEnumerable<DonorDTO>? Output { get; set; }

        public IActionResult OnPostGetXboxTokensAsync()
        {
            string authorizationUrl = authUseCase.GenerateAuthorizationUrl();

            return Redirect(authorizationUrl);
        }

        /// <summary>
        /// Возвращает всех доноров
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGet()
        {
            Output = await authUseCase.GetAllDonors();
            return Page();
        }
    }
}
