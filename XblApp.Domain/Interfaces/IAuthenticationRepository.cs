using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveAsync(TokenOAuth tokenXbl);
        public Task SaveAsync(TokenXau tokenXbl);
        public Task SaveAsync(TokenXsts tokenXbl);
        public Task<TokenOAuth> GetTokenOAuth();
        public DateTime GetDateXstsTokenExpired();
        public DateTime GetDateXauTokenExpired();
        public string GetAuthorizationHeaderValue();
    }
}
