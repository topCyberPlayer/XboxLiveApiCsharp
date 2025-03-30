using XblApp.Domain.Entities;

namespace XblApp.DTO
{
    public class GamerGameAchievementDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public List<GameAchievementDTO2> GameAchievements { get; set; }

        public static GamerGameAchievementDTO CastTo(List<GamerAchievement> gamerAchDb)
        {
            GamerGameAchievementDTO gamerGameAchievement = new()
            {
                GamerId = gamerAchDb.FirstOrDefault().GamerId,
                Gamertag = gamerAchDb.FirstOrDefault().GamerLink.Gamertag,
                GameAchievements = gamerAchDb.Select(a => new GameAchievementDTO2()
                {
                    GameId = a.GameLink.GameId,
                    GameName = a.GameLink.GameName,
                    Achievements = new List<GamerAchievementInnerDTO>()
                    {
                        new GamerAchievementInnerDTO()
                        {
                            Name = a.AchievementLink.Name,
                            Score = a.AchievementLink.Gamerscore,
                            Description = a.AchievementLink.Description,
                            IsUnlocked = a.IsUnlocked
                        }
                    }
                }).ToList()
            };

            return gamerGameAchievement;
        }
    }

    public class GameAchievementDTO2
    {
        public long GameId { get; set; }
        public string GameName { get; set; }
        public List<GamerAchievementInnerDTO> Achievements { get; set; }
    }

    public class GamerAchievementInnerDTO : GameAchievementInnerDTO
    {
        public bool IsUnlocked { get; set; }
    }
}
