namespace Domain.Entities.XblAuth
{
    /// <summary>
    /// 2-й токен. Живет 4 дня
    /// </summary>
    public partial class XboxXauToken
    {
        /// <summary>
        /// User Hash — Уникальный хеш пользователя в системе Xbox Live
        /// </summary>
        public string? UhsId { get; set; }
        /// <summary>
        /// Время выдачи токена
        /// </summary>
        public DateTime IssueInstant { get; set; }
        /// <summary>
        /// Время истечения токена
        /// </summary>
        public DateTime NotAfter { get; set; }
        /// <summary>
        /// Нужен для получения Xbox Live User Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Внешний ключ (ссылается на 1-й токен)
        /// </summary>
        public string UserIdFK { get; set; } = null!;
        /// <summary>
        /// Связь с 1-м токеном
        /// </summary>
        public XboxAuthToken XboxOAuthTokenLink { get; set; } = null!;
        /// <summary>
        /// Связь с 3-м токеном
        /// </summary>
        public XboxXstsToken? XboxXstsTokenLink { get; set; }
    }
}
