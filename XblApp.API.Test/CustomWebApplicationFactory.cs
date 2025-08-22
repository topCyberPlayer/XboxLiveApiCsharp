using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using XblApp.Infrastructure.Contexts;

namespace XblApp.API.Test
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
    {
        public JwtTokenFactory JwtTokenFactory { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                IWebHostEnvironment env = context.HostingEnvironment;

                configBuilder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

                IConfiguration configuration = configBuilder.Build();
                //JwtTokenFactory = new JwtTokenFactory(configuration);
            });

            //builder.ConfigureServices(services =>
            //{
            //    //Замена реальной БД на InMemory
            //    var descriptor = services.SingleOrDefault(
            //        d => d.ServiceType == 
            //        typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));

            //    if (descriptor != null)
            //        services.Remove(descriptor);

            //    services.AddDbContext<ApplicationDbContext>(options =>
            //    {
            //        options.UseInMemoryDatabase("TestDb"); 
            //    });

            //    // services.RemoveAll<ISmsSender>();
            //    // services.AddScoped<ISmsSender, FakeSmsSender>();

            //    var sp = services.BuildServiceProvider();

            //    using var scope = sp.CreateScope();
            //    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    db.Database.EnsureCreated();

            //    SeedTestData(db);
            //});


        }

        private static void SeedTestData(ApplicationDbContext context)
        {
            // context.YourEntities.Add(new YourEntity { ... });
            // context.SaveChanges();
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Это необходимо для поддержки запуска тестов параллельно
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            return base.CreateHost(builder);
        }
    }
}
