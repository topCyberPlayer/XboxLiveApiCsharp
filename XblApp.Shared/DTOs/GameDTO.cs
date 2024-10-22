namespace XblApp.Shared.DTOs
{
    public class GameDTO
    {
        public long GamerId { get; set; }
        public List<TitleDTO> Titles { get; set; }
    }

    public class TitleDTO
    {
        public long TitleId { get; set; }
        public string GameName { get; set; }
        public AchievementDTO Achievement { get; set; }
        public StatsDTO Stats { get; set; }
        public GamePassDTO GamePass { get; set; }
        public List<ImageDTO> Images { get; set; }
        public TitleHistoryDTO TitleHistory { get; set; }
        public DetailDTO Detail { get; set; }
        public FriendsWhoPlayedDTO FriendsWhoPlayed { get; set; }
        public List<ContentBoardDTO> ContentBoards { get; set; }
        public List<ProductIdsWithDeviceTypeDTO> ProductIdsWithDeviceTypes { get; set; }
        public string ProductId { get; set; }
        public List<string> ProductIds { get; set; }
        public string Pfn { get; set; }
        public Guid BingId { get; set; }
        public string ServiceConfigId { get; set; }
        public object WindowsPhoneProductId { get; set; }
        public string Type { get; set; }
        public List<string> Devices { get; set; }
        public string DisplayImage { get; set; }
        public string MediaItemType { get; set; }
        public string ModernTitleId { get; set; }
        public bool IsBundle { get; set; }
        public object TitleRecord { get; set; }
        public List<object> AlternateTitleIds { get; set; }
        public string XboxLiveTier { get; set; }
        public bool? IsStreamable { get; set; }
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

    public class AttributeDTO
    {
        public List<string> ApplicablePlatforms { get; set; }
        public int? Maximum { get; set; }
        public int? Minimum { get; set; }
        public string Name { get; set; }
    }

    public class AvailabilityDTO
    {
        public List<string> Actions { get; set; }
        public string AvailabilityId { get; set; }
        public List<string> Platforms { get; set; }
        public string SkuId { get; set; }
        public string ProductId { get; set; }
    }

    public class ContentBoardDTO
    {
        public List<string> InteractiveElements { get; set; }
        public List<string> RatingDescriptors { get; set; }
        public List<string> RatingDisclaimers { get; set; }
        public string RatingSystem { get; set; }
        public string RatingId { get; set; }
    }

    public class DetailDTO
    {
        public List<AttributeDTO> Attributes { get; set; }
        public List<AvailabilityDTO> Availabilities { get; set; }
        public List<string> Capabilities { get; set; }
        public string Description { get; set; }
        public string DeveloperName { get; set; }
        public List<string> Genres { get; set; }
        public int MinAge { get; set; }
        public string PublisherName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ShortDescription { get; set; }
        public string VuiDisplayName { get; set; }
        public bool XboxLiveGoldRequired { get; set; }
    }

    public class FriendsWhoPlayedDTO
    {
        public List<object> People { get; set; }
        public int CurrentlyPlayingCount { get; set; }
        public int HavePlayedCount { get; set; }
    }

    public class GamePassDTO
    {
        public bool IsGamePass { get; set; }
    }

    public class ImageDTO
    {
        public string Url { get; set; }
        public string Type { get; set; }
    }

    public class ProductIdsWithDeviceTypeDTO
    {
        public string ProductId { get; set; }
        public List<string> Devices { get; set; }
    }

    

    public class StatsDTO
    {
        public int SourceVersion { get; set; }
    }

    public class TitleHistoryDTO
    {
        public DateTime LastTimePlayed { get; set; }
        public bool Visible { get; set; }
        public bool CanHide { get; set; }
    }
}
