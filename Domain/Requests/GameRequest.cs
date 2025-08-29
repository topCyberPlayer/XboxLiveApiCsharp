using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class GameRequest
    {
        [Required] public required string GameName { get; set; }
        [Required] public required int TotalAchievements { get; set; }
        [Required] public required int TotalGamerscore { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Description { get; set; }
        [Required] public ICollection<AchievementRequest> Achievements { get; set; } = [];
    }
}
