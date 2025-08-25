using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace XblApp.API.Test
{
    public class RegisterUser(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        [Theory]
        [InlineData("Aa", "admin@booble.net", "Biba#Boba34","adminTeam")]
        public async Task Register(string userName, string email, string password, string role)
        {
            using (var scope = factory.Services.CreateScope())
            {
                UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                ApplicationUser user = new() { UserName = userName, Email = email };
                IdentityResult result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
