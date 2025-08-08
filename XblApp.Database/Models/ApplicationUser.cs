using Microsoft.AspNetCore.Identity;

namespace XblApp.Database.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; }
    }
}
