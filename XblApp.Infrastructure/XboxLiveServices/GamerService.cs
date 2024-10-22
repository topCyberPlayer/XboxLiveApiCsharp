using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.XboxLiveServices.Models;
using XblApp.Shared.DTOs;

namespace XblApp.Infrastructure.XboxLiveServices
{
    public class GamerService : BaseService, IXboxLiveGamerService
    {
        private static string DefScopes
        {
            get
            {
                return string.Join(",",
                    ProfileSettings.ACCOUNT_TIER,
                    ProfileSettings.APP_DISPLAY_NAME,
                    ProfileSettings.APP_DISPLAYPIC_RAW,
                    ProfileSettings.BIOGRAPHY,
                    ProfileSettings.GAME_DISPLAYPIC_RAW,
                    ProfileSettings.GAME_DISPLAY_NAME,
                    ProfileSettings.GAMERSCORE,
                    ProfileSettings.GAMERTAG,
                    ProfileSettings.PUBLIC_GAMERPIC,
                    ProfileSettings.MODERN_GAMERTAG,
                    ProfileSettings.MODERN_GAMERTAG_SUFFIX,
                    ProfileSettings.PREFERRED_COLOR,
                    ProfileSettings.LOCATION,
                    ProfileSettings.REAL_NAME,
                    ProfileSettings.REAL_NAME_OVERRIDE,
                    ProfileSettings.IS_QUARANTINED,
                    ProfileSettings.TENURE_LEVEL,
                    ProfileSettings.SHOW_USER_AS_AVATAR,
                    ProfileSettings.UNIQUE_MODERN_GAMERTAG,
                    ProfileSettings.XBOX_ONE_REP,
                    ProfileSettings.WATERMARKS);
            }
        }

        public GamerService(IHttpClientFactory factory) : base(factory) { }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag, string authorizationHeaderValue)
        {
            string relativeUrl = $"/users/gt({gamertag})/profile/settings";

            return await GetProfileBase(relativeUrl, authorizationHeaderValue);
        }

        public async Task<GamerDTO> GetGamerProfileAsync(long xuid, string authorizationHeaderValue)
        {
            string relativeUrl = $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBase(relativeUrl, authorizationHeaderValue);
        }

        private async Task<GamerDTO> GetProfileBase(string relativeUrl, string authorizationHeaderValue)
        {
            string? uri = QueryHelpers.AddQueryString(relativeUrl, "settings", DefScopes);

            HttpClient client = factory.CreateClient("gamerService");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("XBL3.0", authorizationHeaderValue);

            HttpResponseMessage response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                //throw new Exception($"Error retrieving profile: {response.ReasonPhrase}");
            }

            GamerJson result = await DeserializeJson<GamerJson>(response);

            return new GamerDTO
            {
                GamerId = long.Parse(result.ProfileUsers.FirstOrDefault().ProfileId),
                Gamertag = result.ProfileUsers.FirstOrDefault().Gamertag,
                Gamerscore = result.ProfileUsers.FirstOrDefault().Gamerscore,
                Bio = result.ProfileUsers.FirstOrDefault().Bio,
                Location = result.ProfileUsers.FirstOrDefault().Location,
                
            };
        }
    }
}
