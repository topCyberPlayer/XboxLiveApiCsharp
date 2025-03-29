namespace XblApp.Domain.Entities
{
    public class GamerAchievement
    {
        public long GamerId { get; set; }
        public long AchievementId { get; set; }
        public long GameId { get; set; }
        public DateTime? DateUnlocked { get; set; }
        public bool IsUnlocked { get; set; }

        
        public Gamer GamerLink { get; set; } = null!; // Навигационное свойство
        public Achievement AchievementLink { get; set; } = null!; // Навигационное свойство
        public Game GameLink { get; set; } = null!; // Навигационное свойство
    }
}

