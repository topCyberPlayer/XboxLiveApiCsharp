namespace XblApp.Domain.Entities.XblAuth
{
    /// <summary>
    /// Живет 16 часов
    /// </summary>
    public partial class XboxXstsToken
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public string? Xuid { get; set; } = null!;
        /// <summary>
        /// Время выдачи профиля
        /// </summary>
        public DateTime IssueInstant { get; set; }
        /// <summary>
        /// Время истечения данных
        /// </summary>
        public DateTime NotAfter { get; set; }
        /// <summary>
        /// Этот токен является 2й частью авторизации в xboxLive API. 
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// Этот токен является 1й частью авторизации в xboxLive API.
        /// </summary>
        public string? Userhash { get; set; }
        public string? Gamertag { get; set; }
        public string? AgeGroup { get; set; }
        public string? Privileges { get; set; }
        public string? UserPrivileges { get; set; }

        /// <summary>
        /// Внешний ключ (ссылается на 2-й токен)
        /// </summary>
        public string UhsIdFK { get; set; } = null!;
        /// <summary>
        /// Связь со 2-м токеном
        /// </summary>
        public XboxXauToken XboxXauTokenLink { get; set; } = null!;
    }
}
