using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seed
{
    public static class StartupExtensions
    {
        public static async Task InitializeInfrastructureIdentityAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole<int>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            foreach (RoleType role in Enum.GetValues<RoleType>())
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role.ToString()));
                }
            }

            string defaultUserGamertag = app.Configuration.GetValue<string>("DefaultUser:Gamertag") ?? throw new ArgumentNullException("Не заполнено поле в appsetting: DefaultUser:Gamertag");
            string defaultUserEmail = app.Configuration.GetValue<string>("DefaultUser:Email") ?? throw new ArgumentNullException("Не заполнено поле в appsetting: DefaultUser:Email");
            string defaultUserPassword = app.Configuration.GetValue<string>("DefaultUser:Password") ?? throw new ArgumentNullException("Не заполнено поле в appsetting: DefaultUser:Password");

            if (await userManager.FindByNameAsync(defaultUserGamertag) is null)
            {
                ApplicationUser user = new()
                {
                    UserName = defaultUserGamertag,
                    Email = defaultUserEmail,
                };

                await userManager.CreateAsync(user, defaultUserPassword);
                await userManager.AddToRoleAsync(user, RoleType.Admin.ToString());

                Gamer gamer = new()
                {
                    GamerId = 1,
                    ApplicationUserId = user.Id,
                    Gamertag = defaultUserGamertag,
                };

                await context.Gamers.AddRangeAsync(gamer);
                await context.SaveChangesAsync();
            }
        }
    }

}
