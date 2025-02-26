using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Domain.Entities;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class AuthenticationRepositoryTests
    {
        private readonly DbContextOptions<XblAppDbContext> _options;
        public AuthenticationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<XblAppDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        private XblAppDbContext CreateContext()
        {
            // Создаем новый контекст для каждой "сессии"
            return new XblAppDbContext(_options);
        }

        [Fact()]
        public async Task SaveTokenOAuthTest_ShouldSave()
        {
            string userId = "TestUserIdHU";
            string accessToken = "Test/Acess;token";

            XboxOAuthToken tokenOAuth = new XboxOAuthToken
            {
                TokenType = "bearer",
                ExpiresIn = 3600,
                Scope = "XboxLive.signin XboxLive.offline_access",
                AccessToken = accessToken,
                RefreshToken = "Test.Refresh-Token",
                AuthenticationToken = "Test_Authentication.Token",
                UserId = userId,
            };

            using (var context = CreateContext())
            {
                var repository = new AuthenticationRepository(context);

                //await repository.SaveTokenAsync(tokenOAuth);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                XboxOAuthToken tokenOAuthRespone = context.XboxOAuthTokens.FirstOrDefault(u => u.UserId == userId);

                Assert.NotNull(tokenOAuthRespone);
                Assert.Equal(accessToken, tokenOAuthRespone.AccessToken);
            }
        }
    }
}
