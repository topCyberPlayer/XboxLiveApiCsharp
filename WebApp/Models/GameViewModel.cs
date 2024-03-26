using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class GameViewModel
    {
        public string Xuid { get; set; }
        public string TitleId { get; set; }
        public string TitleName { get; set; }
        public int TotalGamerscore { get; set; }
        public int TotalAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public int CurrentAchievements { get; set; }
        public string Description { get; set; }
    }
}
