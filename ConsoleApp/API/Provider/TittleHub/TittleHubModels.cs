using System.Text.Json.Serialization;

namespace ConsoleApp.API.Provider.TittleHub
{
    public class TitleHubResponse
    {
        [JsonPropertyName("xuid")]
        public string Xuid { get; set; }

        [JsonPropertyName("titles")]
        public List<Title> Titles { get; set; }
    }

    public class Title
    {
        [JsonPropertyName("titleId")]
        public string TitleId { get; set; }

        [JsonPropertyName("pfn")]
        public string Pfn { get; set; }

        [JsonPropertyName("bingId")]
        public string BingId { get; set; }

        [JsonPropertyName("windowsPhoneProductId")]
        public object WindowsPhoneProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("devices")]
        public List<string> Devices { get; set; }

        [JsonPropertyName("displayImage")]
        public string DisplayImage { get; set; }

        [JsonPropertyName("mediaItemType")]
        public string MediaItemType { get; set; }

        [JsonPropertyName("modernTitleId")]
        public string ModernTitleId { get; set; }

        [JsonPropertyName("isBundle")]
        public bool IsBundle { get; set; }

        [JsonPropertyName("achievement")]
        public Achievement Achievement { get; set; }

        [JsonPropertyName("stats")]
        public object Stats { get; set; }

        [JsonPropertyName("gamePass")]
        public object GamePass { get; set; }

        [JsonPropertyName("images")]
        public object Images { get; set; }

        [JsonPropertyName("titleHistory")]
        public TitleHistory TitleHistory { get; set; }

        [JsonPropertyName("titleRecord")]
        public object TitleRecord { get; set; }

        [JsonPropertyName("detail")]
        public object Detail { get; set; }

        [JsonPropertyName("friendsWhoPlayed")]
        public object FriendsWhoPlayed { get; set; }

        [JsonPropertyName("alternateTitleIds")]
        public object AlternateTitleIds { get; set; }

        [JsonPropertyName("contentBoards")]
        public object ContentBoards { get; set; }

        [JsonPropertyName("xboxLiveTier")]
        public string XboxLiveTier { get; set; }

        [JsonPropertyName("isStreamable")]
        public bool IsStreamable { get; set; }
    }

    public class Achievement
    {
        [JsonPropertyName("currentAchievements")]
        public int CurrentAchievements { get; set; }

        [JsonPropertyName("totalAchievements")]
        public int TotalAchievements { get; set; }

        [JsonPropertyName("currentGamerscore")]
        public int CurrentGamerscore { get; set; }

        [JsonPropertyName("totalGamerscore")]
        public int TotalGamerscore { get; set; }

        [JsonPropertyName("progressPercentage")]
        public double ProgressPercentage { get; set; }

        [JsonPropertyName("sourceVersion")]
        public int SourceVersion { get; set; }
    }

    public class TitleHistory
    {
        [JsonPropertyName("lastTimePlayed")]
        public DateTime LastTimePlayed { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("canHide")]
        public bool CanHide { get; set; }
    }

    static class TitleHubSettings
    {
        internal static string ACHIEVEMENT = "achievement";
        internal static string ALTERNATE_TITLE_ID = "alternateTitleId";
        internal static string CONTENT_BOARD = "contentBoard";
        internal static string SERVICE_CONFIG_ID = "scid";
        internal static string STATS = "stats";
        internal static string GAME_PASS = "gamepass";
        internal static string IMAGE = "image";
        internal static string DETAIL = "detail";
        internal static string FRIENDS_WHO_PLAYED = "friendswhoplayed";
        internal static string PRODUCT_ID = "productId";
    }
}
