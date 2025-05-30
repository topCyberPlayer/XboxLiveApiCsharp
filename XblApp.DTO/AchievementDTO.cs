using XblApp.Domain.Entities;

namespace XblApp.DTO
{
    public class AchievementDTO : GameDTO
    {
        public List<AchievementInnerDTO> Achievements { get; set; }

        public static AchievementDTO? CastTo(Game? gameDb)
        {
            if (gameDb == null) 
                return new AchievementDTO();

            AchievementDTO achievementDTO = new()
            {
                GameId = gameDb.GameId,
                GameName = gameDb.GameName,
                TotalGamerscore = gameDb.TotalGamerscore,
                TotalAchievements = gameDb.TotalAchievements,
                Gamers = gameDb.GamerGameLinks.Count(),
                Achievements = gameDb.AchievementLinks.Select(x => new AchievementInnerDTO()
                {
                    Name = x.Name,
                    Description = x.Description,
                    Score = x.Gamerscore,
                }).ToList()
            };

            return achievementDTO;
        }
    }

    public class AchievementInnerDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Score { get; set; }
    }
}
