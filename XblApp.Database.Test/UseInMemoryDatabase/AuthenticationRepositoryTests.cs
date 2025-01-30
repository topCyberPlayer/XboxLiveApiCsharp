using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Domain.Entities;

namespace XblApp.Database.Test.UseInMemoryDatabase
{
    public class AuthenticationRepositoryTests
    {
        private readonly DbContextOptions<MsSqlDbContext> _options;
        public AuthenticationRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<MsSqlDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
        }

        private MsSqlDbContext CreateContext()
        {
            // Создаем новый контекст для каждой "сессии"
            return new MsSqlDbContext(_options);
        }

        [Fact()]
        public async Task SaveTokenOAuthTest_ShouldSave()
        {
            string aspNetUser = "1";
            string accessToken = "Test/Acess;token";

            TokenOAuth tokenOAuth = new TokenOAuth
            {
                TokenType = "bearer",
                ExpiresIn = 3600,
                Scope = "XboxLive.signin XboxLive.offline_access",
                AccessToken = accessToken,
                RefreshToken = "Test.Refresh-Token",
                AuthenticationToken = "Test_Authentication.Token",
                UserId = "TestUserIdHU",
                AspNetUserId = aspNetUser
            };

            using (var context = CreateContext())
            {
                var repository = new AuthenticationRepository(context);

                await repository.SaveTokenAsync(tokenOAuth);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                TokenOAuth tokenOAuthRespone = context.OAuthTokens.FirstOrDefault(u => u.AspNetUserId == aspNetUser);

                Assert.NotNull(tokenOAuthRespone);
                Assert.Equal(accessToken, tokenOAuthRespone.AccessToken);
            }
        }
    }
}
