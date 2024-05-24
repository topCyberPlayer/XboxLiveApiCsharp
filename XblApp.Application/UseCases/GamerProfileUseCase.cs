using XblApp.Domain.Interfaces;

namespace XblApp.Application.UseCases
{
    public class GamerProfileUseCase
    {
        private readonly IGamerRepository _gamerRepository;
        private readonly IXboxLiveGamerService _xblGamerSrv;

        public GamerProfileUseCase(IGamerRepository gamerRepository, IXboxLiveGamerService xboxLiveGamerService)
        {
            _gamerRepository = gamerRepository;
            _xblGamerSrv = xboxLiveGamerService;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag)
        {
            var gamer = _gamerRepository.GetGamerProfile(gamertag);
            if (gamer == null)
            {
                gamer = await _xblGamerSrv.GetGamerProfileAsync(gamertag);
                _gamerRepository.SaveGamer(gamer);
            }

            return new GamerDTO
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                Bio = gamer.Bio,
                Location = gamer.Location
            };
        }

        public async Task<List<GamerDTO>> GetAllGamerProfilesAsync()
        {
            var gamers = await _gamerRepository.GetAllGamerProfilesAsync();

            return gamers.Select(g => new GamerDTO()
            {
                 GamerId = g.GamerId,
                 Gamertag = g.Gamertag,
                 Gamerscore = g.Gamerscore,
                 CurrentGamesCount = g.GameLinks.Select(a => a.Game).Count(),
                 CurrentAchievementsCount = g.GameLinks.Sum(a => a.CurrentAchievements)
             })
            .ToList();
        }
    }
}
