namespace ServiceLayer.Models
{
    public class GamerModelDto
    {
        public long GamerId { get; set; }

        public string? Gamertag { get; set; }

        public int Gamerscore { get; set; }

        public int CurrentGames { get; set; }

        public int CurrentAchievements { get; set; }
    }
}
