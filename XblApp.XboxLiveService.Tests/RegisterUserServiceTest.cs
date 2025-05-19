using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using XblApp.Database.Contexts;
using XblApp.Domain.Interfaces;
using XblApp.Domain.Interfaces.IXboxLiveService;
using XblApp.Domain.JsonModels;

namespace XblApp.XboxLiveService.Tests
{
    public class RegisterUserServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RegisterUserServiceTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((_, config) =>
                {
                    config.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["Database:useNoSqlDb"] = "true"
                    });
                });

                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IXboxLiveGamerService, XboxLiveGamerServiceMock>();

                    services.RemoveAll(typeof(DbContextOptions<XblAppDbContext>));
                    services.AddDbContext<XblAppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });
                });
            });
        }


        [Theory]
        [InlineData("abcd", "g1@gmail.com", "pass1234&worD")]
        public async Task CreateUserAsync_ShouldOk(string gamertag, string email, string password)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IRegisterUserService service = scope.ServiceProvider.GetRequiredService<IRegisterUserService>();

            (bool Success, string UserId, IEnumerable<string> Errors) result = await service.CreateUserAsync(gamertag, email, password);

            Assert.True(result.Success);
            Assert.NotEmpty(result.UserId);
        }

        [Theory]
        [InlineData("abcd", "g1@gmail.com", "pass1234&worD")]
        public async Task CreateUserAsync_ShouldError_EmailRegisteredYet(string gamertag, string email, string password)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IRegisterUserService service = scope.ServiceProvider.GetRequiredService<IRegisterUserService>();

            (bool Success, string UserId, IEnumerable<string> Errors) result = await service.CreateUserAsync(gamertag, email, password);
            (bool Success, string UserId, IEnumerable<string> Errors) result2 = await service.CreateUserAsync(gamertag, email, password);

            Assert.False(result2.Success);
        }

        [Theory]
        [InlineData("abcd", "g2@gmail.com", "pass1234&worD")]
        public async Task CreateUserAsync_ShouldError_GamertagRegisteredYet(string gamertag, string email, string password)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IRegisterUserService service = scope.ServiceProvider.GetRequiredService<IRegisterUserService>();

            (bool Success, string UserId, IEnumerable<string> Errors) result = await service.CreateUserAsync(gamertag, email, password);
            (bool Success, string UserId, IEnumerable<string> Errors) result2 = await service.CreateUserAsync(gamertag, email, password);

            Assert.False(result2.Success);
        }

        [Theory]
        [InlineData("abcd", "g1@gmail.com", "pass1234word")]
        public async Task CreateUserAsync_ShouldError_WeakPassword(string gamertag, string email, string password)
        {
            IServiceScope scope = _factory.Services.CreateScope();
            IRegisterUserService service = scope.ServiceProvider.GetRequiredService<IRegisterUserService>();

            (bool Success, string UserId, IEnumerable<string> Errors) result = await service.CreateUserAsync(gamertag, email, password);

            Assert.False(result.Success);
        }
    }

    internal class XboxLiveGamerServiceMock : IXboxLiveGamerService
    {
        public Task<GamerJson> GetGamerProfileAsync(string gamertag)
        {
            GamerJson gamerJson = new()
            {
                ProfileUsers = new List<ProfileUser>
                {
                    new ProfileUser("11",gamertag, 110)
                }
            };

            return Task.FromResult(gamerJson);
        }

        public Task<GamerJson> GetGamerProfileAsync(long xuid)
        {
            throw new NotImplementedException();
        }
    }
}
