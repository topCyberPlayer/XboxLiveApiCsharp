using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task SaveTokenAsync(TokenOAuth tokenXbl);
        public Task SaveTokenAsync(TokenXau tokenXbl);
        public Task SaveTokenAsync(TokenXsts tokenXbl);
        public Task<TokenOAuth> GetTokenOAuth();
        public DateTime GetDateXstsTokenExpired();
        public DateTime GetDateXauTokenExpired();
        public string GetAuthorizationHeaderValue();
    }
}
