﻿using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class GamerProfileUseCase
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IXboxLiveGamerService _gamerService;
        private readonly IGamerRepository _gamerRepository;
        

        public GamerProfileUseCase(
            IAuthenticationRepository authRepository, 
            IGamerRepository gamerRepository, 
            IXboxLiveGamerService gamerService)
        {
            _gamerRepository = gamerRepository;
            _gamerService = gamerService;
            _authRepository = authRepository;
        }

        public async Task<GamerDTO> GetGamerProfileAsync(string gamertag)
        {
            Gamer gamer = await _gamerRepository.GetGamerProfileAsync(gamertag);

            return new GamerDTO
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Gamerscore = gamer.Gamerscore,
                Bio = gamer.Bio,
                Location = gamer.Location,
                CurrentGamesCount = gamer.GameLinks.Select(x => x.Game).Count(),
                CurrentAchievementsCount = gamer.GameLinks.Sum(x => x.CurrentAchievements)
            };
        }

        public async Task<List<GamerDTO>> GetAllGamerProfilesAsync()
        {
            List<Gamer> gamers = await _gamerRepository.GetAllGamerProfilesAsync();

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

        public async Task<GamerGameDTO> GetGamesForGamerAsync(string gamertag)
        {
            Gamer gamer = await _gamerRepository.GetGamesForGamerAsync(gamertag);

            return new GamerGameDTO
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Games = gamer.GameLinks.Select(gg => new AbcDTO
                {
                    GameId = gg.Game.GameId,
                    GameName = gg.Game.GameName,
                    TotalAchievements = gg.Game.TotalAchievements,
                    TotalGamerscore = gg.Game.TotalGamerscore,
                    CurrentAchievements = gg.CurrentAchievements,
                    CurrentGamerscore = gg.CurrentGamerscore
                }).ToList()
            };
        }

        public async Task<GamerDTO?> UpdateProfileAsync(string gamertag)
        {
            string authorizationHeaderValue = _authRepository.GetAuthorizationHeaderValue();//todo вызвать другой метод

            GamerDTO response = await _gamerService.GetGamerProfileAsync(gamertag, authorizationHeaderValue);

            Gamer saveGamer = new Gamer()
            {
                GamerId = response.GamerId,
                Gamertag = response.Gamertag,
                Gamerscore = response.Gamerscore,
                Bio = response.Bio,
                Location = response.Location
            };

             await _gamerRepository.SaveGamerAsync(saveGamer);

            return response;
        }
    }
}
