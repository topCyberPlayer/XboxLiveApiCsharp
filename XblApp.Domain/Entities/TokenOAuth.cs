using System.ComponentModel.DataAnnotations;

namespace XblApp.Domain.Entities
{
    public partial class TokenOAuth
    {
        [Key]
        [Required]
        public string? AspNetUserId { get; set; }
        public string? UserId { get; set; }
        public string? TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string? Scope { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? AuthenticationToken { get; set; }
    }
}
