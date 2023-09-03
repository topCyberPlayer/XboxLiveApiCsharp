using ConsoleApp.Authentication;
using ConsoleApp.Database;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Store
{
    internal class StorageDb : IStorage
    {
        public async Task SaveToken<T>(T value)
        {
            if (value is OAuth2TokenResponse)
            {
                OAuth2TokenResponse? typeValue = value as OAuth2TokenResponse;

                using (AppDbContext appDbContext = new())
                {
                    appDbContext.OAuth2TokenResponses.Add(typeValue);
                    //appDbContext.OAuth2TokenResponses.Update(typeValue);
                    int result = await appDbContext.SaveChangesAsync();
                    return;
                }
            }

            if (value is XSTSResponse)
            {
                XSTSResponse? typeValue = value as XSTSResponse;

                using (AppDbContext appDbContext = new())
                {
                    appDbContext.XSTSResponses.Add(typeValue);
                    int result = await appDbContext.SaveChangesAsync();
                    return;
                }
            }
        }

        public async Task<T> GetToken<T>()
        {
            Object? response = null;

            if (typeof(T) == typeof(OAuth2TokenResponse))
            {
                using (AppDbContext appDbContext = new AppDbContext())
                {
                    response = await appDbContext.OAuth2TokenResponses.FirstOrDefaultAsync();
                    return (T)response;
                }
            }

            if (typeof(T) == typeof(XSTSResponse))
            {
                using (AppDbContext appDbContext = new AppDbContext())
                {
                    response = await appDbContext.XSTSResponses.FirstOrDefaultAsync();
                    return (T)response;
                }
            }

            return (T)response;
        }
    }
}
