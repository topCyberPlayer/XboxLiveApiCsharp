using System.Text.Json.Serialization;

namespace XblApp.Domain.Entities.JsonModels
{
    public class GamerJson 
    {
        [JsonPropertyName("profileUsers")]
        public ICollection<ProfileUser> ProfileUsers { get; set; } = [];
    }

    public class ProfileUser
    {
        public ProfileUser(string applicationUserId, string gamertag, int gamerscore)
        {
            ApplicationUserId = applicationUserId;
            Gamertag = gamertag;
            Gamerscore = gamerscore;
        }

        /// <summary>
        /// Кастомное поле. Заполняется вручную
        /// </summary>
        public string ApplicationUserId { get; set; } = null!;

        [JsonPropertyName("id")]
        public long GamerId { get; set; }

        [JsonPropertyName("hostId")]
        public long HostId { get; set; }

        [JsonPropertyName("settings")]
        public List<Setting>? Settings { get; set; } = [];

        [JsonPropertyName("isSponsoredUser")]
        public bool IsSponsoredUser { get; set; }

        public string Gamertag
        {
            get => GetSetting(ProfileSettings.GAMERTAG);
            set => SetSetting(ProfileSettings.GAMERTAG, value);
        }

        public int Gamerscore
        {
            get => int.TryParse(GetSetting(ProfileSettings.GAMERSCORE), out var score) ? score : 0;
            set => SetSetting(ProfileSettings.GAMERSCORE, value.ToString());
        }

        public string? Location
        {
            get => GetSetting(ProfileSettings.LOCATION);
            set => SetSetting(ProfileSettings.LOCATION, value);
        }

        public string? Bio
        {
            get => GetSetting(ProfileSettings.BIOGRAPHY);
            set => SetSetting(ProfileSettings.BIOGRAPHY, value);
        }

        public int TenureLevel
        {
            get => int.TryParse(GetSetting(ProfileSettings.TENURE_LEVEL), out var score) ? score : 0;
            set => SetSetting(ProfileSettings.TENURE_LEVEL, value.ToString());
        }

        public string? XboxOneRep
        {
            get => GetSetting(ProfileSettings.XBOX_ONE_REP);
            set => SetSetting(ProfileSettings.XBOX_ONE_REP, value);
        }

        public string? RealName
        {
            get => GetSetting(ProfileSettings.REAL_NAME);
            set => SetSetting(ProfileSettings.REAL_NAME, value);
        }

        private string? GetSetting(string key) => Settings.FirstOrDefault(s => s.Id == key)?.Value;

        private void SetSetting(string key, string? value)
        {
            var setting = Settings.FirstOrDefault(s => s.Id == key);
            if (setting != null)
                setting.Value = value ?? string.Empty;
            else if (value != null)
                Settings.Add(new Setting { Id = key, Value = value });
        }
    }

    public class Setting
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }

    public static class ProfileSettings
    {
        public static string ACCOUNT_TIER = "AccountTier";
        public static string APP_DISPLAY_NAME = "AppDisplayName";
        public static string APP_DISPLAYPIC_RAW = "AppDisplayPicRaw";
        public static string BIOGRAPHY = "Bio";
        public static string GAME_DISPLAYPIC_RAW = "GameDisplayPicRaw";
        public static string GAME_DISPLAY_NAME = "GameDisplayName";
        public static string GAMERSCORE = "Gamerscore";
        public static string GAMERTAG = "Gamertag";
        public static string PUBLIC_GAMERPIC = "PublicGamerpic";
        public static string MODERN_GAMERTAG = "ModernGamertag";
        public static string MODERN_GAMERTAG_SUFFIX = "ModernGamertagSuffix";
        public static string PREFERRED_COLOR = "PreferredColor";
        public static string LOCATION = "Location";
        public static string REAL_NAME = "RealName";
        public static string REAL_NAME_OVERRIDE = "RealNameOverride";
        public static string IS_QUARANTINED = "IsQuarantined";
        public static string TENURE_LEVEL = "TenureLevel";
        public static string SHOW_USER_AS_AVATAR = "ShowUserAsAvatar";
        public static string UNIQUE_MODERN_GAMERTAG = "UniqueModernGamertag";
        public static string XBOX_ONE_REP = "XboxOneRep";
        public static string WATERMARKS = "Watermarks";
    }
}
