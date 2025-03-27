namespace XblApp.Domain.Entities
{
    public class GamerAchievement
    {
        public long GamerId { get; set; }
        public Gamer GamerLink { get; set; } = null!; // Навигационное свойство
        public DateTimeOffset DateUnlocked { get; set; }
        public bool IsUnlocked { get; set; }

        public long AchievementId { get; set; }
        public Achievement AchievementLink { get; set; } = null!; // Навигационное свойство

        
    }
}
