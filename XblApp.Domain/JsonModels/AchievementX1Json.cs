using System.Text.Json.Serialization;

namespace XblApp.Domain.JsonModels
{
    public class AchievementX1Json
    {
        [JsonPropertyName("achievements")]
        public ICollection<AchievementInnerJson> Achievements { get; set; }
        
        [JsonPropertyName("pagingInfo")]
        public PagingInfo PagingInfo { get; set; }

        public long GamerId { get; set; }
    }

    public class AchievementInnerJson
    {
        [JsonPropertyName("id")]
        public string AchievementId { get; set; }

        [JsonPropertyName("serviceConfigId")]
        public string ServiceConfigId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("titleAssociations")]
        public List<TitleAssociation> TitleAssociations { get; set; }

        [JsonPropertyName("progressState")]
        public string ProgressState { get; set; }

        [JsonPropertyName("progression")]
        public Progression Progression { get; set; }

        [JsonPropertyName("mediaAssets")]
        public List<MediaAsset> MediaAssets { get; set; }

        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        [JsonPropertyName("isSecret")]
        public bool? IsSecret { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("lockedDescription")]
        public string LockedDescription { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("achievementType")]
        public string AchievementType { get; set; }

        [JsonPropertyName("participationType")]
        public string ParticipationType { get; set; }

        [JsonPropertyName("timeWindow")]
        public TimeWindow TimeWindow { get; set; }

        [JsonPropertyName("rewards")]
        public List<Reward> Rewards { get; set; }

        [JsonPropertyName("estimatedTime")]
        public string EstimatedTime { get; set; }

        [JsonPropertyName("deeplink")]
        public string Deeplink { get; set; }

        [JsonPropertyName("isRevoked")]
        public bool? IsRevoked { get; set; }
    }

    public class MediaAsset
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Progression
    {
        [JsonPropertyName("requirements")]
        public List<Requirement> Requirements { get; set; }

        [JsonPropertyName("timeUnlocked")]
        public DateTime? TimeUnlocked { get; set; }
    }

    public class Requirement
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("current")]
        public object Current { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }
    }

    public class Reward
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("valueType")]
        public string ValueType { get; set; }
    }

    public class TimeWindow
    {
        [JsonPropertyName("startDate")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime? EndDate { get; set; }
    }

    public class TitleAssociation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
