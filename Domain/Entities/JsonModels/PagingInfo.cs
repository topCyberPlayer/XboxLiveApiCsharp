using System.Text.Json.Serialization;

namespace XblApp.Domain.Entities.JsonModels
{
    public class PagingInfo
    {
        [JsonPropertyName("continuationToken")]
        public string? ContinuationToken { get; set; }

        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
    }
}
