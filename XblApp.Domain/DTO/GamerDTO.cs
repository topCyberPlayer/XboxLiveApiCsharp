namespace XblApp.Domain.DTO
{
    public class GamerDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        /// <summary>
        /// Сумма всех достижений в каждой игре
        /// </summary>
        public int TotalAchievementsInGame { get; set; }
        /// <summary>
        /// Сумма всех игр
        /// </summary>
        public int TotalGames { get; set; }
    }
}
