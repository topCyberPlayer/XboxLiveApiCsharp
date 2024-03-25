
using WebApp.Pages.Profile;

namespace WebApp.Services
{
    public class TittleHubService : AuthenticationService
    {
        public TittleHubService(IConfiguration configuration, HttpClient httpClient2, AuthenticationServiceXbl authServXbl, AuthenticationServiceDb authServDb) : base(configuration, httpClient2, authServXbl, authServDb)
        {
            _xServiceUrl = configuration["ConnectionStrings:TittleHubServiceUrl"];
        }

        public Task<ProfileViewModel> GetTitleHistory(string userId, string xuid, int maxItems = 5)
        {
            string requestUri = _xServiceUrl + $"/api/TitleHub/GetTitleHistory?xuid={xuid}&maxItems={maxItems}";

            return base.GetBaseMethod<ProfileViewModel>(userId, requestUri);
        }
    }
}
