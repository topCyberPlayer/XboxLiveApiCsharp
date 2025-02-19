using Microsoft.AspNetCore.Identity;

namespace XblApp.UI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CrearedAt { get; set; }
    }
}
