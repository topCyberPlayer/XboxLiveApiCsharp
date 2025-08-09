using Microsoft.AspNetCore.Identity;

namespace XblApp.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
