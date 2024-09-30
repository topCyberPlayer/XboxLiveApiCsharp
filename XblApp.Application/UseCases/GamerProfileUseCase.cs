using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class GamerProfileUseCase : BaseUseCase
    {
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        public GamerProfileUseCase(
            IAuthenticationRepository authRepository, 
            IGamerRepository gamerRepository, 
            IXboxLiveGamerService gamerService,
            IAuthenticationService authService) : base(authService, authRepository)
        {
            _gamerRepository = gamerRepository;
            _gamerService = gamerService;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag)
        {
            GamerDTO gamer = await _gamerRepository.GetGamerProfileAsync(gamertag);

            return gamer;

            //return new GamerDTO
            //{
            //    GamerId = gamer.GamerId,
            //    Gamertag = gamer.Gamertag,
            //    Gamerscore = gamer.Gamerscore,
            //    Bio = gamer.Bio,
            //    Location = gamer.Location,
            //    CurrentGamesCount = gamer.GameLinks.Select(x => x.Game).Count(),
            //    CurrentAchievementsCount = gamer.GameLinks.Sum(x => x.CurrentAchievements)
            //};
        }

        public async Task<List<GamerDTO>> GetAllGamerProfilesAsync()
        {
            List<GamerDTO> gamers = await _gamerRepository.GetAllGamerProfilesAsync();

            return gamers;
        }

        public async Task<GamerGameDTO> GetGamesForGamerAsync(string gamertag)
        {
            GamerGameDTO gamesForGamer = await _gamerRepository.GetGamesForGamerAsync(gamertag);

            return gamesForGamer;
        }

        public async Task<GamerDTO?> UpdateProfileAsync(string gamertag)
        {
            if (IsDateXstsTokenExperid())
            {
                //Если дата XstsToken истекла, то запрашиваю из БД OAuthToken и обновляю его

                TokenOAuthDTO experidTokenOAuth = await _authRepository.GetTokenOAuth();

                await RefreshTokens(experidTokenOAuth);
            }

            string authorizationHeaderValue = _authRepository.GetAuthorizationHeaderValue();

            GamerDTO response = await _gamerService.GetGamerProfileAsync(gamertag, authorizationHeaderValue);

            await _gamerRepository.SaveGamerAsync(response);

            return response;
        }

        public bool IsDateXstsTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXstsTokenExpired();

            return dateNow > dateDb ? true : false;
        }

        public bool IsDateXauTokenExperid()
        {
            DateTime? dateNow = DateTime.Now;

            DateTime? dateDb = _authRepository.GetDateXauTokenExpired();

            return dateNow > dateDb ? true : false;
        }
    }
}
