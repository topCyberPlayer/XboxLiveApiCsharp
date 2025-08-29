using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser<int>//По умолчанию ApplicationUserId имеет тип string, но мы переопределили его на int, чтобы использовать int в качестве первичного ключа
    {
        public ApplicationUser()
        {
            SecurityStamp = Guid.NewGuid().ToString();//Из-за SecurityStamp не заполняется по умолчанию
        }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Gamer Gamer { get; set; } = null!;
    }
}
