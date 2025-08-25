namespace Domain.DTO
{
    public class AchievementDTO : GameDTO
    {
        public IEnumerable<AchievementInnerDTO>? Achievements { get; set; }
    }

    public class AchievementInnerDTO
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public int Score { get; set; }
    }
}
