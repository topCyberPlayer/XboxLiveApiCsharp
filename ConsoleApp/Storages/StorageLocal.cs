using ConsoleApp.API.Provider.Profile;
using ConsoleApp.API.Provider.TittleHub;
using ConsoleApp.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp.Store
{
    internal class StorageLocal : IStorage
    {       
        private const string _filePathA = "OpenXbox\\xbox";

        private string _filePathOauth2;
        private const string _nameOauth2 = "1_oauth2_tokens.json";

        private string _filePathXau;
        private const string _nameXau = "2_xau_tokens.json";


        private string _filePathXsts;
        private const string _nameXsts = "3_xsts_tokens.json";

        private string _filePathProfile;
        private const string _nameProfile = "Profile.json";

        private string _fileTitleHub;
        private const string _nameTitleHub = "TitleHub.json";


        public StorageLocal()
        {
            string _filePathLocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);//same: "C:\Users\<username>\AppData\Local"

            _filePathOauth2 = Path.Combine(_filePathLocalAppData, _filePathA, _nameOauth2);
            _filePathXau = Path.Combine(_filePathLocalAppData, _filePathA, _nameXau);
            _filePathXsts = Path.Combine(_filePathLocalAppData, _filePathA, _nameXsts);
            _filePathProfile = Path.Combine(_filePathLocalAppData, _filePathA, _nameProfile);
            _fileTitleHub = Path.Combine(_filePathLocalAppData, _filePathA, _nameTitleHub);
        }

        public async Task SaveToken<T>(T value)
        {
            string filePath = default;

            if (value is OAuth2TokenResponse) filePath = _filePathOauth2;
            if (value is XAUResponse) filePath = _filePathXau;
            if (value is XSTSResponse) filePath = _filePathXsts;
            if (value is ProfileResponse) filePath = _filePathProfile;
            if (value is TitleHubResponse) filePath = _fileTitleHub;

            if(filePath != default)
            {
                JsonSerializerOptions _options = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var options = new JsonSerializerOptions(_options)
                {
                    WriteIndented = true
                };

                await using FileStream fileStream = File.Create(filePath);
                await JsonSerializer.SerializeAsync(fileStream, value, options);
            }
        }

        public async Task<T> GetToken<T>()
        {
            string filePath = default;
            T? oAuth2_response = default;

            if (typeof(T) == typeof(OAuth2TokenResponse)) filePath = _filePathOauth2;
            if (typeof(T) == typeof(XAUResponse)) filePath = _filePathXau;
            if (typeof(T) == typeof(XSTSResponse)) filePath = _filePathXsts;

            if (File.Exists(filePath))
            {
                await using FileStream json = File.OpenRead(filePath);
                oAuth2_response = await JsonSerializer.DeserializeAsync<T>(json);
            }

            return oAuth2_response;
        }

        
    }
}
