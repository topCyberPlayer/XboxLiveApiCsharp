﻿using System.Text.Json.Serialization;

namespace ProfileService.Profiles
{
    public class ProfileModelXbl
    {
        [JsonPropertyName("profileUsers")]
        public IEnumerable<ProfileUser> ProfileUsers { get; set; }
    }

    public class ProfileUser
    {
        [JsonPropertyName("id")]
        public string ProfileId { get; set; }

        [JsonPropertyName("hostId")]
        public string HostId { get; set; }

        [JsonPropertyName("settings")]
        public List<Setting> Settings { get; set; }

        [JsonPropertyName("isSponsoredUser")]
        public bool IsSponsoredUser { get; set; }

        public string Gamertag { get { return Settings.FirstOrDefault(s => s.Id == ProfileSettings.GAMERTAG).Value; } }

        public int Gamerscore { get { return int.Parse(Settings.FirstOrDefault(s => s.Id == ProfileSettings.GAMERSCORE).Value); } }
    }

    public class Setting
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
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
