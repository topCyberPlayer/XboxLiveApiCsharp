using System.ComponentModel.DataAnnotations;

namespace XblApp.Shared.DTOs
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
        /// Кол-во игроков играющие в данную игру
        /// </summary>
        public int Gamers { get; set; }
    }
}
