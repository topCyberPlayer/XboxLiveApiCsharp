using System.Text.Json.Serialization;

namespace XblApp.Domain.Entities.JsonModels
{
    public class AchievementX360Json
    {
        [JsonPropertyName("achievements")]
        public ICollection<AchievementX360InnerJson> Achievements { get; set; }

        [JsonPropertyName("version")]
        public DateTime Version { get; set; }

        [JsonPropertyName("pagingInfo")]
        public PagingInfo PagingInfo { get; set; }

        public long GamerId { get; set; }
    }

    public class AchievementX360InnerJson
    {
        [JsonPropertyName("id")]
        public int AchievementId { get; set; }

        [JsonPropertyName("titleId")]
        public long GameId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("unlockedOnline")]
        public bool UnlockedOnline { get; set; }

        [JsonPropertyName("unlocked")]
        public bool Unlocked { get; set; }

        [JsonPropertyName("isSecret")]
        public bool IsSecret { get; set; }

        [JsonPropertyName("gamerscore")]
        public int Gamerscore { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("lockedDescription")]
        public string LockedDescription { get; set; }

        [JsonPropertyName("isRevoked")]
        public bool IsRevoked { get; set; }

        [JsonPropertyName("timeUnlocked")]
        public DateTime TimeUnlocked { get; set; }
    }
}