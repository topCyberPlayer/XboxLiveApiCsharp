namespace XblApp.DTO
{
    public class GamerGameDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public List<GameInnerDTO> Games { get; set; }
    }

    public class GameInnerDTO
    {
        public long GameId { get; set; }
        public string? GameName { get; set; }
        public int TotalAchievements { get; set; }
        public int TotalGamerscore { get; set; }
        public int CurrentAchievements { get; set; }
        public int CurrentGamerscore { get; set; }

        /// <summary>
        /// Прогресс достижений
        /// </summary>
        public double AchievementsProgress => TotalAchievements == 0 ? 0 : (double)CurrentAchievements / TotalAchievements;

    }

}
