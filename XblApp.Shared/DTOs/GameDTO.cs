namespace XblApp.Shared.DTOs
{
    public class GameDTO
    {
        public long Xuid { get; set; }
        public List<TitleDTO> Titles { get; set; }
    }

    public class TitleDTO
    {
        public string TitleId { get; set; }
        public string ProductId { get; set; }
        public List<string> ProductIds { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DisplayImage { get; set; }
        public bool IsBundle { get; set; }
        public AchievementDTO Achievement { get; set; }
        public StatsDTO Stats { get; set; }
        // Add other necessary properties here
    }

    public class AchievementDTO
    {
        public int CurrentAchievements { get; set; }
        public int TotalAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public int TotalGamerscore { get; set; }
        public double ProgressPercentage { get; set; }
        public int SourceVersion { get; set; }
    }

    public class StatsDTO
    {
        public int SourceVersion { get; set; }
    }
}
