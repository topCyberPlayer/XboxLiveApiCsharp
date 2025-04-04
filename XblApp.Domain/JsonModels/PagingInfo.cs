using System.Text.Json.Serialization;

namespace XblApp.Domain.JsonModels
{
    public class PagingInfo
    {
        [JsonPropertyName("continuationToken")]
        public string? ContinuationToken { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
    }
}
