using XblApp.Domain.DTO;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.Interfaces.Repository;

namespace XblApp.Application.XboxLiveUseCases
{
    public class GamerProfileUseCase(
        IXboxLiveGamerService gamerService,
        IGamerRepository gamerRepository,
        IXboxLiveGameService gameService,
        IGameRepository gameRepository,
        IXboxLiveAchievementService<AchievementX360Json> achievementX360Service,
        IXboxLiveAchievementService<AchievementX1Json> achievementX1Service,
        IAchievementRepository achievementRepository)
    {
        private readonly IXboxLiveGamerService _gamerService = gamerService;
        private readonly IGamerRepository _gamerRepository = gamerRepository;

        private readonly IXboxLiveGameService _gameService = gameService;
        private readonly IGameRepository _gameRepository = gameRepository;

        private readonly IXboxLiveAchievementService<AchievementX360Json> _achievementX360Service = achievementX360Service;
        private readonly IXboxLiveAchievementService<AchievementX1Json> _achievementX1Service = achievementX1Service;
        private readonly IAchievementRepository _achievementRepository = achievementRepository;

        public async Task<Gamer> GetGamerProfileRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamerProfileAsync(gamertag);

        public async Task<IEnumerable<GamerDTO>> GetAllGamerProfilesRepoAsync() =>
            await _gamerRepository.GetInclude_GamerGame_Game_Async(
                a => new GamerDTO()
                {
                    GamerId = a.GamerId,
                    Gamertag = a.Gamertag,
                    Gamerscore = a.Gamerscore,
                    Bio = a.Bio,
                    Location = a.Location,
                    TotalGames = a.GamerGameLinks.Count,
                    TotalAchievementsInGame = a.GamerGameLinks.Sum(x => x.CurrentAchievements)
                });

        public async Task<Gamer> GetGamesForGamerRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamesForGamerAsync(gamertag);

        public async Task<List<GamerAchievement>> GetGamerAchievementsAsync(string gamertag) =>
            await _achievementRepository.GetGamerAchievementsAsync(gamertag);

       

        public async Task UpdateProfileAsync(long gamerId)
        {
            GamerJson gamers = await GetAndSaveGamerProfile(gamerId);

            GameJson games = await GetAndSaveGames(gamerId);

            (AchievementX1Json achievementsX1, AchievementX360Json achievementsX360) = await GetAndSaveAchievements(gamerId);
        }

        public async Task<GamerJson> GetAndSaveGamerProfile(long gamerId)
        {
            GamerJson gamers = await _gamerService.GetGamerProfileAsync(gamerId);
            await _gamerRepository.SaveOrUpdateGamersAsync(gamers);

            return gamers;
        }

        public async Task<GameJson> GetAndSaveGames(long gamerId)
        {
            GameJson games = await _gameService.GetGamesForGamerProfileAsync(gamerId);
            await _gameRepository.SaveOrUpdateGamesAsync(games);

            return games;
        }

        public async Task<(AchievementX1Json, AchievementX360Json)> GetAndSaveAchievements(long gamerId)
        {
            AchievementX1Json achievementsX1 = await _achievementX1Service.GetAllAchievementsForGamerAsync(gamerId);
            await _achievementRepository.SaveAchievementsAsync(achievementsX1);

            AchievementX360Json achievementsX360 = await _achievementX360Service.GetAllAchievementsForGamerAsync(gamerId);
            await _achievementRepository.SaveAchievementsAsync(achievementsX360);

            return (achievementsX1, achievementsX360);
        }
    }
}