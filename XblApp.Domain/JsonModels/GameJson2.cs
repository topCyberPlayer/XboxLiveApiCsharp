using System.Text.Json.Serialization;

namespace XblApp.Domain.JsonModels
{
    public class GameJson2
    {
        [JsonPropertyName("titles")]
        public ICollection<TitleA> Titles { get; set; }

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

    public class TitleA
    {
        [JsonPropertyName("titleId")]
        public long TitleId { get; set; }
        
        [JsonPropertyName("lastUnlock")]
        public DateTimeOffset LastUnlock { get; set; }
        
        [JsonPropertyName("serviceConfigId")]
        public string ServiceConfigId { get; set; }
        
        [JsonPropertyName("titleType")]
        public string TitleType { get; set; }
        
        [JsonPropertyName("platform")]
        public string Platform { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("earnedAchievements")]
        public int EarnedAchievements { get; set; }

        [JsonPropertyName("currentGamerscore")]
        public int CurrentGamerscore { get; set; }

        [JsonPropertyName("maxGamerscore")]
        public int MaxGamerscore { get; set; }
    }
}