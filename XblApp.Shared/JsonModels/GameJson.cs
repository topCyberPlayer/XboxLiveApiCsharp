using System.Text.Json.Serialization;
using XblApp.Domain.Entities;

namespace XblApp.DTO.JsonModels
{
    public class GameJson
    {
        [JsonPropertyName("xuid")]
        public string Xuid { get; set; }

        [JsonPropertyName("titles")]
        public ICollection<Title> Titles { get; set; }

        public List<Game> MapTo()
        {
            return Titles.Select(title => new Game
            {
                GameId = long.TryParse(title.TitleId, out long gameId) ? gameId : throw new FormatException($"Invalid TitleId format for Game: {title.Name}"),
                GameName = title.Name ?? throw new ArgumentException("Game name cannot be null."),
                TotalAchievements = title.Achievement?.TotalAchievements ?? 0,
                TotalGamerscore = title.Achievement?.TotalGamerscore ?? 0,
                ReleaseDate = title.Detail.ReleaseDate != null ? DateOnly.FromDateTime((DateTime)title.Detail.ReleaseDate) : null,
                GamerGameLinks = new List<GamerGame>()
                    {
                        new GamerGame
                        {
                            GamerId = long.TryParse(Xuid, out var gamerId) ? gamerId : throw new FormatException($"Invalid GamerId format for Game: {title.Name}"),
                            CurrentAchievements = title.Achievement.CurrentAchievements,
                            CurrentGamerscore = title.Achievement.CurrentGamerscore,
                            LastTimePlayed = title.TitleHistory.LastTimePlayed
                        }
                    }
            }).ToList();
        }
            
    }

    public class Title
    {
        [JsonPropertyName("titleId")]
        public string TitleId { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("productIds")]
        public List<string> ProductIds { get; set; }

        [JsonPropertyName("productIdsWithDeviceTypes")]
        public List<ProductIdsWithDeviceType> ProductIdsWithDeviceTypes { get; set; }

        [JsonPropertyName("pfn")]
        public string Pfn { get; set; }

        [JsonPropertyName("bingId")]
        public string BingId { get; set; }

        [JsonPropertyName("serviceConfigId")]
        public string ServiceConfigId { get; set; }

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
        public GameAchievement Achievement { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("gamePass")]
        public GamePass GamePass { get; set; }

        [JsonPropertyName("images")]
        public List<Image> Images { get; set; }

        [JsonPropertyName("titleHistory")]
        public TitleHistory TitleHistory { get; set; }

        [JsonPropertyName("titleRecord")]
        public object TitleRecord { get; set; }

        [JsonPropertyName("detail")]
        public Detail Detail { get; set; }

        [JsonPropertyName("friendsWhoPlayed")]
        public FriendsWhoPlayed FriendsWhoPlayed { get; set; }

        [JsonPropertyName("alternateTitleIds")]
        public List<object> AlternateTitleIds { get; set; }

        [JsonPropertyName("contentBoards")]
        public List<ContentBoard> ContentBoards { get; set; }

        [JsonPropertyName("xboxLiveTier")]
        public string XboxLiveTier { get; set; }

        [JsonPropertyName("isStreamable")]
        public bool? IsStreamable { get; set; }
    }

    public class GameAchievement
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

    public class Attribute
    {
        [JsonPropertyName("applicablePlatforms")]
        public List<string> ApplicablePlatforms { get; set; }

        [JsonPropertyName("maximum")]
        public int? Maximum { get; set; }

        [JsonPropertyName("minimum")]
        public int? Minimum { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Availability
    {
        [JsonPropertyName("Actions")]
        public List<string> Actions { get; set; }

        [JsonPropertyName("AvailabilityId")]
        public string AvailabilityId { get; set; }

        [JsonPropertyName("Platforms")]
        public List<string> Platforms { get; set; }

        [JsonPropertyName("SkuId")]
        public string SkuId { get; set; }

        [JsonPropertyName("ProductId")]
        public string ProductId { get; set; }
    }

    public class ContentBoard
    {
        [JsonPropertyName("InteractiveElements")]
        public List<string> InteractiveElements { get; set; }

        [JsonPropertyName("RatingDescriptors")]
        public List<string> RatingDescriptors { get; set; }

        [JsonPropertyName("RatingDisclaimers")]
        public List<string> RatingDisclaimers { get; set; }

        [JsonPropertyName("RatingSystem")]
        public string RatingSystem { get; set; }

        [JsonPropertyName("RatingId")]
        public string RatingId { get; set; }
    }

    public class Detail
    {
        [JsonPropertyName("attributes")]
        public List<Attribute> Attributes { get; set; }

        [JsonPropertyName("availabilities")]
        public List<Availability> Availabilities { get; set; }

        [JsonPropertyName("capabilities")]
        public List<string> Capabilities { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("developerName")]
        public string DeveloperName { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("minAge")]
        public int MinAge { get; set; }

        [JsonPropertyName("publisherName")]
        public string PublisherName { get; set; }

        [JsonPropertyName("releaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("vuiDisplayName")]
        public string VuiDisplayName { get; set; }

        [JsonPropertyName("xboxLiveGoldRequired")]
        public bool XboxLiveGoldRequired { get; set; }
    }

    public class FriendsWhoPlayed
    {
        [JsonPropertyName("people")]
        public List<object> People { get; set; }

        [JsonPropertyName("currentlyPlayingCount")]
        public int CurrentlyPlayingCount { get; set; }

        [JsonPropertyName("havePlayedCount")]
        public int HavePlayedCount { get; set; }
    }

    public class GamePass
    {
        [JsonPropertyName("isGamePass")]
        public bool IsGamePass { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class ProductIdsWithDeviceType
    {
        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("devices")]
        public List<string> Devices { get; set; }
    }

    public class Stats
    {
        [JsonPropertyName("sourceVersion")]
        public int SourceVersion { get; set; }
    }

    public class TitleHistory
    {
        [JsonPropertyName("lastTimePlayed")]
        public DateTimeOffset LastTimePlayed { get; set; }

        [JsonPropertyName("visible")]
        public bool Visible { get; set; }

        [JsonPropertyName("canHide")]
        public bool CanHide { get; set; }
    }

    public static class TitleHubSettings
    {
        public const string ACHIEVEMENT = "achievement";
        public const string ALTERNATE_TITLE_ID = "alternateTitleId";
        public const string CONTENT_BOARD = "contentBoard";
        public const string SERVICE_CONFIG_ID = "scid";
        public const string STATS = "stats";
        public const string GAME_PASS = "gamepass";
        public const string IMAGE = "image";
        public const string DETAIL = "detail";
        public const string FRIENDS_WHO_PLAYED = "friendswhoplayed";
        public const string PRODUCT_ID = "productId";
    }
}
