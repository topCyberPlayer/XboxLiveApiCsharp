using System.Text.Json.Serialization;

namespace XblApp.DTO.JsonModels
{
    public class AchievementJson
    {
        [JsonPropertyName("achievements")]
        public ICollection<TitleW> Titles { get; set; }

        [JsonPropertyName("pagingInfo")]
        public PagingInfo PagingInfos { get; set; }
    }

    public class PagingInfo
    {
        [JsonPropertyName("continuationToken")]
        public string? ContinuationToken { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
    }

    public class TitleW
    {
        public long TitleId { get; set; }
        public DateTimeOffset LastUnlock { get; set; } 
        public string ServiceConfigId { get; set; }
        public string TitleType { get; set; }
        public string Platform { get; set; }
        public string Name { get; set; }
        public int EarnedAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public int MaxGamerscore { get; set; }
    }
}
