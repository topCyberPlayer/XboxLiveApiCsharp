using System.ComponentModel.DataAnnotations;

namespace XblApp.Domain.Entities
{
    public partial class TokenXau
    {
        [Key]
        [Required]
        public string? AspNetUserId { get; set; }

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
        /// User Hash — Уникальный хеш пользователя в системе Xbox Live
        /// </summary>
        public string? Uhs { get; set; }
    }
}
