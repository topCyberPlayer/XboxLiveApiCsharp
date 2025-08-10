namespace XblApp.Domain.DTO
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
        public int TotalGamers { get; set; }
    }
}
