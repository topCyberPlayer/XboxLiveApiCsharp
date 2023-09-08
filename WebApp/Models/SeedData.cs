using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WebAppContext>>()))
            {

                DbSet<IdentityUserToken<string>> userTokens = context.UserTokens;
            }
        }
    }
}
