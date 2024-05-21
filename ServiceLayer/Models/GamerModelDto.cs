namespace ServiceLayer.Models
{
    public class GamerModelDto
    {
        public int GamerId { get; set; }

        public string? Gamertag { get; set; }

        public int Gamerscore { get; set; }

        public int CurrentGames { get; set; }

        public int CurrentAchievements { get; set; }
    }
}
