namespace Domain.Entities.XblAuth
{
    /// <summary>
    /// // 1-й токен. Этот файл содержит стандартные OAuth-токены, используемые для авторизации в Microsoft Identity Platform
    /// </summary>
    public partial class XboxAuthToken
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public string? UserId { get; set; } = null!;
        public string? TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string? Scope { get; set; }
        /// <summary>
        /// Нужен для получения Xbox Live Token
        /// </summary>
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
        public XboxXauToken? XboxXauTokenLink { get; set; }
    }
}
