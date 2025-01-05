using Microsoft.EntityFrameworkCore;

namespace XblApp.Infrastructure.Data.Seeding
{
    public static class DatabaseStartupHelpers
    {
        public static async Task<WebApplication> SetupDatabaseAsync(this WebApplication app)
        {
            // Initialize the database with seed data
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IWebHostEnvironment>();
                var context = scope.ServiceProvider.GetRequiredService<XblAppDbContext>();

                try
                {
                    var arePendingMigrations = context.Database.GetPendingMigrations().Any();
                    await context.Database.MigrateAsync();

                    await context.SeedDatabaseIfNoGamersAsync(env.WebRootPath);
                    await context.SeedDatabaseIfNoGamesAsync(env.WebRootPath);
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            return app;
        }
    }
}
