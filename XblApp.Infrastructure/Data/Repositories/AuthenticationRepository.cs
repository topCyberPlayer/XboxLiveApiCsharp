using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Infrastructure.Data.Repositories
{
    public class AuthenticationRepository : BaseService, IAuthenticationRepository
    {
        public AuthenticationRepository(XblAppDbContext context) : base(context)
        {
        }

        public string GetAuthorizationHeaderValue()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateXauTokenExpired()
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateXstsTokenExpired()
        {
            throw new NotImplementedException();
        }

        public Task<TokenOAuth> GetTokenOAuth()
        {
            throw new NotImplementedException();
        }

        public Task SaveTokenAsync(TokenOAuth tokenXbl)
        {
            throw new NotImplementedException();
        }

        public Task SaveTokenAsync(TokenXau tokenXbl)
        {
            throw new NotImplementedException();
        }

        public Task SaveTokenAsync(TokenXsts tokenXbl)
        {
            throw new NotImplementedException();
        }
    }
}
