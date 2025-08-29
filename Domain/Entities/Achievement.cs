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

        public long GameId { get; set; }
        public Game? GameLink { get; set; }// Навигационное свойство для связи с игрой
        public ICollection<GamerAchievement>? GamerAchievementLinks { get; set; }
    }
}
