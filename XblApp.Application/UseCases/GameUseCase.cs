﻿using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;
using XblApp.Shared.DTOs;

namespace XblApp.Application.UseCases
{
    public class GameUseCase : BaseUseCase
    {
        private readonly IGameRepository _gameRepository;

        public GameUseCase(
            IGameRepository gameRepository,
            IAuthenticationService authService,
            IAuthenticationRepository authRepository) : base(authService, authRepository)
        {
            _gameRepository = gameRepository;   
        }

        public async Task<List<TitleDTO>> GetAllGamesAsync()
        {
            List<TitleDTO> games = await _gameRepository.GetAllGamesAsync();

            return games;
            //return games.Select(g => new GameDTO
            //{
            //    GameId = g.GameId,
            //    GameName = g.GameName,
            //    TotalAchievements = g.TotalAchievements,
            //    TotalGamerscore = g.TotalGamerscore,
            //    TotalGamers = g.GamerLinks.Select(a => a.Gamer).Count(),
            //}).ToList();
        }
    }
}
