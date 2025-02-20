using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XblApp.Domain.Entities
{
    public class Achievement
    {
        public long AchievementId { get; set; }
        public long GameId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Gamerscore { get; set; }
        public bool IsSecret { get; set; }
        public DateTimeOffset DateUnlock { get; set; }

        public Game? GameLink { get; set; }// Навигационное свойство для связи с игрой
        public ICollection<GamerAchievement>? GamerLinks { get; set; }
    }
}
