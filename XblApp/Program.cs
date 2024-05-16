using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GamerServices;

namespace XblApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string? dbProvider = builder.Configuration.GetConnectionString("DatabaseProvider");
            builder.Services.RegisterApplicationServices(dbProvider);
            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }

    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, string dbProvider)
        {         
            services.AddScoped<GamerService>();            
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddRazorPages();

            switch (dbProvider)
            {
                case "MsSql":
                    services.AddDbContext<XblAppDbContext, MsSqlDbContext>();
                    break;
                case "PostgreSql":
                    services.AddDbContext<XblAppDbContext, PostgresDbContext>();
                    break;
            }

            return services;

            //services.AddDbContext<XblAppDbContext>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
