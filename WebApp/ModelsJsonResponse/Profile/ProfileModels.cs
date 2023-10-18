using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ModelsJsonResponse.Profile
{
    public class ProfileResponse
    {
        [JsonPropertyName("profileUsers")]
        public List<ProfileUser> ProfileUsers { get; set; }
    }

    public class ProfileUser
    {
        [JsonPropertyName("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProfileId { get; set; }

        [JsonPropertyName("hostId")]
        public string HostId { get; set; }

        [JsonPropertyName("settings")]
        public List<Setting> Settings { get; set; }

        [JsonPropertyName("isSponsoredUser")]
        public bool IsSponsoredUser { get; set; }
    }

    public class Setting
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    static class ProfileSettings
    {
        internal static string ACCOUNT_TIER = "AccountTier";
        internal static string APP_DISPLAY_NAME = "AppDisplayName";
        internal static string APP_DISPLAYPIC_RAW = "AppDisplayPicRaw";
        internal static string BIOGRAPHY = "Bio";
        internal static string GAME_DISPLAYPIC_RAW = "GameDisplayPicRaw";
        internal static string GAME_DISPLAY_NAME = "GameDisplayName";
        internal static string GAMERSCORE = "Gamerscore";
        internal static string GAMERTAG = "Gamertag";
        internal static string PUBLIC_GAMERPIC = "PublicGamerpic";
        internal static string MODERN_GAMERTAG = "ModernGamertag";
        internal static string MODERN_GAMERTAG_SUFFIX = "ModernGamertagSuffix";
        internal static string PREFERRED_COLOR = "PreferredColor";
        internal static string LOCATION = "Location";
        internal static string REAL_NAME = "RealName";
        internal static string REAL_NAME_OVERRIDE = "RealNameOverride";
        internal static string IS_QUARANTINED = "IsQuarantined";
        internal static string TENURE_LEVEL = "TenureLevel";
        internal static string SHOW_USER_AS_AVATAR = "ShowUserAsAvatar";
        internal static string UNIQUE_MODERN_GAMERTAG = "UniqueModernGamertag";
        internal static string XBOX_ONE_REP = "XboxOneRep";
        internal static string WATERMARKS = "Watermarks";
    }
}
