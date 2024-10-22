using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class GamerProfileUseCase : BaseUseCase
    {
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        private readonly IXboxLiveGameService _gameService;
        private readonly IGameRepository _gameRepository;

        public GamerProfileUseCase(
            IAuthenticationService authService,
            IAuthenticationRepository authRepository,
            IXboxLiveGamerService gamerService,
            IGamerRepository gamerRepository,
            IXboxLiveGameService gameService,
            IGameRepository gameRepository) : base(authService, authRepository)
        {
            _gamerRepository = gamerRepository;
            _gamerService = gamerService;

            _gameService = gameService;
            _gameRepository = gameRepository;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag)
        {
            GamerDTO gamer = await _gamerRepository.GetGamerProfileAsync(gamertag);

            return gamer;
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

            GamerDTO gamerResponse = await _gamerService.GetGamerProfileAsync(gamertag, authorizationHeaderValue);
            await _gamerRepository.SaveGamerAsync(gamerResponse);
            
            //Получаю список игр и ИД игрока, которому они принадлежат
            GameDTO gameResponse = await _gameService.GetTitleHistoryAsync(gamertag, authorizationHeaderValue);
            await _gameRepository.SaveGamesAsync(gameResponse);/* Надо сохранить в таблицу: GamerGamer, Games */

            return gamerResponse;
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
