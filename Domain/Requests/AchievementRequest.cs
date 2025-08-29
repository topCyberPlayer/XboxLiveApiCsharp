using System.ComponentModel.DataAnnotations;

namespace Domain.Requests
{
    public class AchievementRequest
    {
        [Required] public required string Name { get; set; }
        [Required] public required string Description { get; set; }
        [Required] public required int Gamerscore { get; set; }
        [Required] public required bool IsSecret { get; set; }
    }
}
