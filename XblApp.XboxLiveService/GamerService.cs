﻿using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.DTO.JsonModels;

namespace XblApp.XboxLiveService
{
    public class GamerService : BaseService, IXboxLiveGamerService
    {
        private static readonly string DefScopes = string.Join(",",
            ProfileSettings.GAMERSCORE
            ,ProfileSettings.GAMERTAG
            //,ProfileSettings.ACCOUNT_TIER
            //,ProfileSettings.APP_DISPLAY_NAME
            //,ProfileSettings.APP_DISPLAYPIC_RAW
            //,ProfileSettings.BIOGRAPHY
            //,ProfileSettings.GAME_DISPLAYPIC_RAW
            //,ProfileSettings.GAME_DISPLAY_NAME
            //,ProfileSettings.PUBLIC_GAMERPIC
            //,ProfileSettings.MODERN_GAMERTAG
            //,ProfileSettings.MODERN_GAMERTAG_SUFFIX
            //,ProfileSettings.PREFERRED_COLOR
            //,ProfileSettings.LOCATION
            //,ProfileSettings.REAL_NAME
            //,ProfileSettings.REAL_NAME_OVERRIDE
            //,ProfileSettings.IS_QUARANTINED
            //,ProfileSettings.TENURE_LEVEL
            //,ProfileSettings.SHOW_USER_AS_AVATAR
            //,ProfileSettings.UNIQUE_MODERN_GAMERTAG
            //,ProfileSettings.XBOX_ONE_REP
            //,ProfileSettings.WATERMARKS
        );

        private readonly ILogger<GamerService> _logger;

        public GamerService(IHttpClientFactory factory, ILogger<GamerService> logger) : base(factory)
        { 
            _logger = logger;
        }

        public async Task<List<Gamer>> GetGamerProfileAsync(string gamertag)
        {
            string relativeUrl = $"/users/gt({gamertag})/profile/settings";

            return await GetProfileBaseAsync(relativeUrl);
        }

        public async Task<List<Gamer>> GetGamerProfileAsync(long xuid)
        {
            string relativeUrl = $"/users/xuid({xuid})/profile/settings";

            return await GetProfileBaseAsync(relativeUrl);
        }

        private async Task<List<Gamer>> GetProfileBaseAsync(string relativeUrl)
        {
            _logger.LogInformation("Fetching profile from URL: {Url}", relativeUrl);

            try
            {
                string? uri = QueryHelpers.AddQueryString(relativeUrl, "settings", DefScopes);

                HttpClient client = factory.CreateClient("GamerService");

                GamerJson result = await SendRequestAsync<GamerJson>(client, uri);

                return GamerJson.MapToGamer(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch profile.");
                throw;
            }
        }

        
    }
}
