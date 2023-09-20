using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.RegisterApplicationServices();

            WebApplication app = builder.Build();
            app.ConfigureMiddleware();
            app.RegisterEndpoints();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("WebAppContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<WebAppContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<WebAppContext>();
            
            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
                microsoftOptions.SaveTokens = true;
            });

            builder.Services.ConfigureApplicationCookie(options =>
                options.Events = new CookieAuthenticationEvents
                {
                    OnSignedIn = async context =>
                    {
                        // Получение данных пользователя
                        var identity = context.Principal.Identity as ClaimsIdentity;
                        var sheme = context.Scheme;
                        var items = context.Response.HttpContext.Items;

                        // Получение и обработка токенов
                        var accessToken = identity?.FindFirst("access_token")?.Value;
                        var refreshToken = identity?.FindFirst("refresh_token")?.Value;

                        // Доступ к сервисам для дополнительной обработки
                        //var myService = context.HttpContext.RequestServices.GetService<MyService>();

                        // Дополнительная логика обработки данных и токенов
                        // ...

                        await Task.CompletedTask;
                    }
                }
                    // Другие события аутентификации, если они нужны.
            );

            return builder.Services;
        }
    }

    public static partial class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            //using(var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    SeedData.Initialize(services);
            //}


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            return app;
        }
    }

    public static partial class EndpointMapper
    {
        public static WebApplication RegisterEndpoints(this WebApplication app)
        {
            app.MapGet("/test", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            //app.MapControllerRoute(
            //    name: "MicrosoftCallback",
            //    pattern: "/Account/MicrosoftCallback"
            //        //defaults: new { controller = "YourControllerName", action = "MicrosoftCallback" }
            //        );

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages(); // Ваша конфигурация маршрутов Razor Pages

            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");

            //});

            return app;
        }
    }
}