namespace XblApp.Shared.DTOs
{
    public class GamerDTO
    {
        public long GamerId { get; set; }
        public string? Gamertag { get; set; }
        public int Gamerscore { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public int CurrentGamesCount { get; set; }
        public int CurrentAchievementsCount { get; set; }
    }
}
