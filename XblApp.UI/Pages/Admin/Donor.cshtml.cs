using Application.XboxLiveUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XblApp.UI.Pages.Admin
{
    [Authorize(Roles = "adminTeam")]
    public class DonorModel : PageModel
    {
        public List<DonorViewModel> Output { get; set; }

        private readonly AuthenticationUseCase _authUseCase;

        public DonorModel(AuthenticationUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        public IActionResult OnPostGetXboxTokensAsync()
        {
            string authorizationUrl = _authUseCase.GenerateAuthorizationUrl();

            return Redirect(authorizationUrl);
        }

        /// <summary>
        /// Возвращает всех доноров
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGet()
        {
            var donors = await _authUseCase.GetAllDonors();
            Output = MapToDonorViewModel(donors);
            return Page();
        }

        private static List<DonorViewModel> MapToDonorViewModel(
            List<(string UserId, DateTime XboxLiveNotAfter, DateTime XboxUserNotAfter, string Xuid, string Gamertag)> donors) =>
            donors.Select(d => new DonorViewModel
            {
                UserId = d.UserId,
                XboxLiveNotAfter = d.XboxLiveNotAfter,
                XboxUserNotAfter = d.XboxUserNotAfter,
                Xuid = d.Xuid,
                Gamertag = d.Gamertag
            }).ToList();
    }

    public class DonorViewModel
    {
        public string UserId { get; set; }

        public DateTime XboxLiveNotAfter { get; set; }

        public DateTime XboxUserNotAfter { get; set; }

        public string Xuid { get; set; }

        public string Gamertag { get; set; }
    }
}
