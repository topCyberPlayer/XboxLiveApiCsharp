namespace XblApp.Domain.Entities
{
    public class GamerAchievement
    {
        public long GamerId { get; set; }
        public long AchievementId { get; set; }
        public DateTime? DateUnlocked { get; set; }
        public bool IsUnlocked { get; set; }

        
        public Achievement AchievementLink { get; set; } = null!; // Навигационное свойство

        public Gamer GamerLink { get; set; } = null!; // Навигационное свойство
    }
}

