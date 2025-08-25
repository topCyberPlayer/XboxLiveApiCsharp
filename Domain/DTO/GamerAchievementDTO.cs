namespace Domain.DTO
{
    public class GamerAchievementDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public IEnumerable<GameAchievementInnerDTO> GameAchievements { get; set; } = [];
    }

    public class GameAchievementInnerDTO : AchievementInnerDTO
    {
        public long GameId { get; set; }


    }
}
