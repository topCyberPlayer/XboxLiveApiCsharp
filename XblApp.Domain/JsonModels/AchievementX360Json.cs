using System.Text.Json.Serialization;

namespace XblApp.Domain.JsonModels
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
        public int AchievementX360Id { get; set; }

        [JsonPropertyName("titleId")]
        public int GameId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sequence")]
        public int sequence { get; set; }

        [JsonPropertyName("flags")]
        public int flags { get; set; }

        [JsonPropertyName("unlockedOnline")]
        public bool unlockedOnline { get; set; }

        [JsonPropertyName("unlocked")]
        public bool unlocked { get; set; }

        [JsonPropertyName("isSecret")]
        public bool isSecret { get; set; }

        [JsonPropertyName("platform")]
        public int platform { get; set; }

        [JsonPropertyName("gamerscore")]
        public int gamerscore { get; set; }

        [JsonPropertyName("imageId")]
        public int imageId { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("lockedDescription")]
        public string lockedDescription { get; set; }

        [JsonPropertyName("type")]
        public int type { get; set; }

        [JsonPropertyName("isRevoked")]
        public bool isRevoked { get; set; }

        [JsonPropertyName("timeUnlocked")]
        public DateTime timeUnlocked { get; set; }
    }
}