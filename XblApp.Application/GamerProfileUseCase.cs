﻿using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces.IRepository;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.Application
{
    public class GamerProfileUseCase : AuthenticationUseCase
    {
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;

        private readonly IXboxLiveGameService _gameService;
        private readonly IGameRepository _gameRepository;

        private readonly IXboxLiveAchievementService _achievementService;
        private readonly IAchievementRepository _achievementRepository;

        public GamerProfileUseCase(
            IXboxLiveAuthenticationService authService,
            IAuthenticationRepository authRepository,
            IXboxLiveGamerService gamerService,
            IGamerRepository gamerRepository,
            IXboxLiveGameService gameService,
            IGameRepository gameRepository,
            IXboxLiveAchievementService achievementService,
            IAchievementRepository achievementRepository) : base(authService, authRepository)
        {
            _gamerService = gamerService;
            _gamerRepository = gamerRepository;

            _gameService = gameService;
            _gameRepository = gameRepository;

            _achievementService = achievementService;
            _achievementRepository = achievementRepository;
        }

        public async Task<Gamer> GetGamerProfileRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamerProfileAsync(gamertag);

        public async Task<List<Gamer>> GetAllGamerProfilesRepoAsync() =>
            await _gamerRepository.GetAllGamerProfilesAsync();

        public async Task<Gamer> GetGamesForGamerRepoAsync(string gamertag) =>
            await _gamerRepository.GetGamesForGamerAsync(gamertag);

        public async Task<List<GamerAchievement>> GetGamerAchievementsAsync(string gamertag) =>
            await _achievementRepository.GetGamerAchievementsAsync(gamertag);

        public async Task UpdateProfileAsync(long gamerId)
        {
            GamerJson gamers = await GetAndSaveGamerProfile(gamerId);

            GameJson games = await GetAndSaveGames(gamerId);

            foreach (var game in games.Titles)
            {
                await GetAndSaveAchievements(gamerId, long.TryParse(game.TitleId, out long gameId) ? gameId : default);
            }
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

        public async Task GetAndSaveAchievements(long gamerId, long gameId)
        {
            AchievementJson achievements = await _achievementService.GetAchievementsAsync(gamerId, gameId);
            await _achievementRepository.SaveAchievementsAsync(achievements);
        }
    }
}