﻿using XblApp.Domain.Entities;

namespace XblApp.DTO
{
    /// <summary>
    /// Представление для игры вне зависимости от игрока
    /// </summary>
    public class GameDTO
    {
        public long GameId { get; set; }
        public string? GameName { get; set; }
        public int TotalAchievements { get; set; }
        public int TotalGamerscore { get; set; }

        /// <summary>
        /// Кол-во игроков сыгравших в данную игру
        /// </summary>
        public int Gamers { get; set; }

        public static IEnumerable<GameDTO> CastToGameDTO(List<Game> gamers) =>
            gamers.Select(MapToGameDTO);

        public static GameDTO? CastToGameDTO(Game game) =>
            game is null ? null : MapToGameDTO(game);

        private static GameDTO MapToGameDTO(Game game) => new()
        {
            GameId = game.GameId,
            GameName = game.GameName,
            Gamers = game.GamerGameLinks.Count,
            TotalAchievements = game.TotalAchievements,
            TotalGamerscore = game.TotalGamerscore,
        };
    }
}
