namespace XblApp.XboxLiveService.Models
{
    public class AchievementJson
    {
        public ICollection<Title> Titles { get; set; }
    }

    public class Title
    {
        public long TitleId { get; set; }
        public string LastUnlock { get; set; } // Используем string, если не хотим парсить дату сразу
        public string ServiceConfigId { get; set; }
        public string TitleType { get; set; }
        public string Platform { get; set; }
        public string Name { get; set; }
        public int EarnedAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public int MaxGamerscore { get; set; }
    }
}
