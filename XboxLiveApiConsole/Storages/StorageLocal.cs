using ConsoleApp.SaveResponses;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp.Storages
{
    internal class StorageLocal : IStorage
    {
        public async Task<T> GetToken<T>(SaveResponse saveResponse)
        {
            T? response = default;

            if (File.Exists(saveResponse.GetFilePath()))
            {
                await using FileStream json = File.OpenRead(saveResponse.GetFilePath());
                response = await JsonSerializer.DeserializeAsync<T>(json);
            }

            return response;
        }

        public async Task SaveToken<T>(SaveResponse saveResponse, T value)
        {
            JsonSerializerOptions _options = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

            var options = new JsonSerializerOptions(_options)
            {
                WriteIndented = true
            };

            await using FileStream fileStream = File.Create(saveResponse.GetFilePath());
            await JsonSerializer.SerializeAsync(fileStream, value, options);
        }
    }
}
