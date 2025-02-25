namespace XblApp.Domain.Entities
{
    /// <summary>
    /// 2-й токен. Живет 4 дня
    /// </summary>
    public partial class XboxLiveToken
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
        /// Token передается в заголовке Authorization в каждом запросе к Xbox API
        /// </summary>
        public string? Token { get; set; }
        
        /// <summary>
        /// Внешний ключ (ссылается на 1-й токен)
        /// </summary>
        public string UserIdFK { get; set; } = null!; 
        /// <summary>
        /// Связь с 1-м токеном
        /// </summary>
        public XboxOAuthToken XboxOAuthTokenLink { get; set; } = null!;
        /// <summary>
        /// Связь с 3-м токеном
        /// </summary>
        public XboxUserToken? UserTokenLink { get; set; }
    }
}
