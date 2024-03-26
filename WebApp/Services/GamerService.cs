using System.Net.Http.Headers;
using WebApp.Models;

namespace WebApp.Services
{
    public class GamerService : AuthenticationService
    {
        GamerViewModel? result = default;

        List<GamerViewModel> _gamers = new List<GamerViewModel>()
        {
            new GamerViewModel()
            {
                Id = "1",
                Gamertag = "aaaa",
                Gamerscore = 111,
                Bio = "Atlanta",
                IsSponsoredUser = true
            },
            new GamerViewModel()
            {
                Id = "2",
                Gamertag = "bbbb",
                Gamerscore = 222,
                Bio = "Boston",
                IsSponsoredUser = false
            }
        };

        public GamerService(IConfiguration configuration,
            HttpClient httpClient2,
            AuthenticationServiceXbl authServXbl,
            AuthenticationServiceDb authServDb) : base(httpClient2, authServXbl, authServDb)
        {
            _xServiceUrl = configuration["ConnectionStrings:ProfileServiceUrl"];
        }

        public GamerViewModel GetProfileByGamertag(string gamertag)
        {
            string requestUri = _xServiceUrl + $"/api/Profile/GetProfileByGamertag?gamertag={gamertag}";

            return _gamers.Where(x => x.Gamertag == gamertag).FirstOrDefault();

            //return base.GetBaseMethod<GamerViewModel>(userId, requestUri);
        }

        internal List<GamerViewModel> GetProfiles()
        {
            return _gamers;
        }
    }
}
