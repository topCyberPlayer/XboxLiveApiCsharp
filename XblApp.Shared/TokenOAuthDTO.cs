namespace XblApp.DTO
{
    public partial class TokenOAuthDTO
    {
        public string? UserId { get; set; }
        public string? TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string? Scope { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? AuthenticationToken { get; set; }
    }
}
