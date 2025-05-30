using Microsoft.EntityFrameworkCore;
using XblApp.Database.Contexts;
using XblApp.Database.Repositories;
using XblApp.Domain.JsonModels;

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

        /// <summary>
        /// Проверяю логику сохранения AuthToken. А также их обновление
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SaveTokenOAuthTest_ShouldSave()
        {
            // Arrange
            OAuthTokenJson? authTokenJson = JsonLoader<OAuthTokenJson>.LoadJsonFile("../../../../", "AuthToken.json");
            XauTokenJson? xauTokenJson = JsonLoader<XauTokenJson>.LoadJsonFile("../../../../", "XauToken.json");
            XstsTokenJson? xstsTokenJson = JsonLoader<XstsTokenJson>.LoadJsonFile("../../../../", "XstsToken.json");

            using (var context = CreateContext())
            {
                var repository = new AuthenticationRepository(context);

                await repository.SaveOrUpdateTokensAsync(authTokenJson, xauTokenJson, xstsTokenJson);
            }

            // Assert (в новом контексте)
            using (var context = CreateContext())
            {
                //XboxAuthToken tokenOAuthRespone = context.XboxOAuthTokens.FirstOrDefault(u => u.UserId == userId);

                //Assert.NotNull(tokenOAuthRespone);
                //Assert.Equal(accessToken, tokenOAuthRespone.AccessToken);
            }
        }
    }
}
