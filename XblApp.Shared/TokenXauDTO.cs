namespace XblApp.DTO
{
    public partial class TokenXauDTO
    {
        public DateTime IssueInstant { get; set; }
        public DateTime NotAfter { get; set; }
        public string? Token { get; set; }
        public string? Uhs { get; set; }
    }
}
