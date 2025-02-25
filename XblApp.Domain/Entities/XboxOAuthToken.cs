using System.ComponentModel.DataAnnotations;

namespace XblApp.Domain.Entities
{
    /// <summary>
    /// // 1-й токен
    /// </summary>
    public partial class XboxOAuthToken 
    {
        public string? UserId { get; set; } = null!; // Ключ
        public string? TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string? Scope { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? AuthenticationToken { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? DateOfIssue { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Дата окончания срока действия
        /// </summary>
        public DateTime? DateOfExpiry { get; set; }

        /// <summary>
        /// Связь с 2-м токеном
        /// </summary>
        public XboxLiveToken? XboxLiveTokenLink { get; set; }
    }
}
