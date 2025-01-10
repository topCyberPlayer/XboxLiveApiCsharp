using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XblApp.Application.UseCases;
using XblApp.Domain.Interfaces;
using XblApp.Infrastructure.Data;
using XblApp.Infrastructure.Data.Repositories;
using XblApp.Infrastructure.Data.Seeding;
using XblApp.Infrastructure.XboxLiveServices;

namespace XblApp
{   
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.RegisterApplicationServices();
            WebApplication? app = builder.Build();

            await app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static WebApplicationBuilder RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>("authServiceAuthToken", (HttpClient client) =>
            {
                client.BaseAddress = new Uri("https://login.live.com/oauth20_token.srf");
            });

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>("authServiceUserToken", (HttpClient client) =>
            {
                client.BaseAddress = new Uri("https://user.auth.xboxlive.com/user/authenticate");
                client.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            });

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>("authServiceXstsToken", (HttpClient client) =>
            {
                client.BaseAddress = new Uri("https://xsts.auth.xboxlive.com/xsts/authorize");
                client.DefaultRequestHeaders.Add("x-xbl-contract-version", "1");
            });

            builder.Services.AddHttpClient<IXboxLiveGamerService, GamerService>("gamerService", (HttpClient client) =>
            {
                client.BaseAddress = new Uri("https://profile.xboxlive.com");
                client.DefaultRequestHeaders.Add("x-xbl-contract-version", "3");
            });

            builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>("gameService", (HttpClient client) =>
            {
                client.BaseAddress = new Uri("https://xsts.auth.xboxlive.com/xsts/authorize");
                client.DefaultRequestHeaders.Add("x-xbl-contract-version", "2");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US");
            });

            builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            builder.Services.AddScoped<IGamerRepository, GamerRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IXboxLiveGamerService, GamerService>();
            builder.Services.AddScoped<IXboxLiveGameService, GameService>();
            builder.Services.AddScoped<AuthenticationUseCase>();
            builder.Services.AddScoped<GamerProfileUseCase>();
            builder.Services.AddScoped<GameUseCase>();
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<XblAppDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();

            string? dbProvider = builder.Configuration.GetConnectionString("DatabaseProvider");

            switch (dbProvider)
            {
                case "MsSql":
                    builder.Services.AddDbContext<XblAppDbContext, MsSqlDbContext>();
                    break;
                case "PostgreSql":
                    builder.Services.AddDbContext<XblAppDbContext, PostgresDbContext>();
                    break;
            }

            return builder;
        }
    }

    public static partial class MiddlewareInitializer
    {
        public static async Task<WebApplication> ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            await app.SetupDatabaseAsync();

            app.UseAuthorization();

            //using(var scope = app.Services.CreateScope())
            //{
            //    var context = scope.ServiceProvider.GetRequiredService<MsSqlDbContext>();
            //    context.Database.Migrate();
            //}

            return app;
        }
    }

    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapRazorPages();

            return app;
        }
    }

    public static class DatabaseStartupHelpers
    {
        public static async Task<WebApplication> SetupDatabaseAsync(this WebApplication app)
        {
            // Initialize the database with seed data
            using (IServiceScope scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = scope.ServiceProvider.GetRequiredService<XblAppDbContext>();

                try
                {
                    bool arePendingMigrations = context.Database.GetPendingMigrations().Any();

                    if (arePendingMigrations)
                        await context.Database.MigrateAsync();

                    await context.SeedDatabaseIfNoGamersAsync();
                    await context.SeedDatabaseIfNoGamesAsync();
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
