using XblApp.Domain.Entities;

namespace XblApp.DTO
{
    public class GamerGameDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public List<GameInnerDTO> Games { get; set; }

        public static GamerGameDTO CastToGamerGameDTO(Gamer gamer) =>
            new GamerGameDTO()
            {
                GamerId = gamer.GamerId,
                Gamertag = gamer.Gamertag,
                Games = gamer.GamerGameLinks.Select(gg => new GameInnerDTO
                {
                    GameId = gg.GameId,
                    GameName = gg.GameLink.GameName,
                    TotalAchievements = gg.GameLink.TotalAchievements,
                    TotalGamerscore = gg.GameLink.TotalGamerscore,
                    CurrentAchievements = gg.CurrentAchievements,
                    CurrentGamerscore = gg.CurrentGamerscore,
                    LastTimePlayed = gg.LastTimePlayed
                }).ToList()
            };
    }

    public class GameInnerDTO
    {
        public long GameId { get; set; }
        public string? GameName { get; set; }
        public int TotalAchievements { get; set; }
        public int TotalGamerscore { get; set; }
        public int CurrentAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public DateTimeOffset LastTimePlayed { get; set; }

        /// <summary>
        /// Прогресс достижений
        /// </summary>
        public double AchievementsProgress => TotalAchievements == 0 ? 
            0 : Math.Floor((double)CurrentAchievements / TotalAchievements * 100);

    }

}
