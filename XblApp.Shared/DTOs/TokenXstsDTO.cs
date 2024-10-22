namespace XblApp.Shared.DTOs
{
    public partial class TokenXstsDTO
    {
        public DateTime IssueInstant { get; set; }
        public DateTime NotAfter { get; set; }
        public string? Token { get; set; }

        public string? Xuid { get; set; }
        public string? Userhash { get; set; }
        public string? Gamertag { get; set; }
        public string? AgeGroup { get; set; }
        public string? Privileges { get; set; }
        public string? UserPrivileges { get; set; }
    }
}
