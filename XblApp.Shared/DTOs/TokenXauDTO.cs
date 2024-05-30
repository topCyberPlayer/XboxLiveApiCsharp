namespace XblApp.Shared.DTOs
{
    public partial class TokenXauDTO
    {
        public string? AspNetUserId { get; set; }
        public DateTime IssueInstant { get; set; }
        public DateTime NotAfter { get; set; }
        public string? Token { get; set; }
        public string? Uhs { get; set; }
    }
}
