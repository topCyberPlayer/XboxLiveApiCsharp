using XblApp.Domain.Entities;

namespace XblApp.DTO
{
    public class GamerDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }

        /// <summary>
        /// Сумма всех достижений в каждой игре
        /// </summary>
        public int Achievements { get; set; }

        /// <summary>
        /// Сумма всех игр
        /// </summary>
        public int Games { get; set; }

        public static List<GamerDTO> CastToToGamerDTO(List<Gamer> gamers) =>
            gamers.Select(MapToGamerDTO).ToList();

        public static GamerDTO CastToGamerDTO(Gamer? gamer) =>
            gamer is null ? null : MapToGamerDTO(gamer);

        private static GamerDTO MapToGamerDTO(Gamer gamer) => new()
        {
            GamerId = gamer.GamerId,
            Gamertag = gamer.Gamertag,
            Gamerscore = gamer.Gamerscore,
            Bio = gamer.Bio,
            Location = gamer.Location,
            Games = gamer.GameLinks.Count,
            Achievements = gamer.GameLinks.Sum(x => x.CurrentAchievements)
        };
    }
}
