using System.ComponentModel.DataAnnotations;

namespace XblApp.Domain.Entities
{
    public partial class TokenXau
    {
        [Key]
        [Required]
        public string? AspNetUserId { get; set; }
        public DateTime IssueInstant { get; set; }
        public DateTime NotAfter { get; set; }
        public string Token { get; set; }
        public string Uhs { get; set; }
    }
}
