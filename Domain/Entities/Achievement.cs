namespace Domain.Entities
{
    public class Achievement
    {
        public required long AchievementId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? LockedDescription { get; set; }
        public required int Gamerscore { get; set; }
        public required bool IsSecret { get; set; }

        /// <summary>
        /// Связь с игрой
        /// </summary>
        public long GameId { get; set; }
        public Game? GameLink { get; set; }

        /// <summary>
        /// Связь с достижениями игрока
        /// </summary>
        public ICollection<GamerAchievement>? GamerAchievementLinks { get; set; } = [];

        /// <summary>
        /// Связь с решениями. Игрок может предлагать несколько решений для получения достижения
        /// </summary>
        public ICollection<Solution>? SolutionLinks { get; set; } = [];
    }
}
