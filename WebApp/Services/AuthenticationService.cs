namespace WebApp.Services
{
    public class AuthenticationService
    {
        private AuthenticationServiceXbl _authServXbl;
        private AuthenticationServiceDb _authServDb;

        //public AuthenticationService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        //{
        //    _authServXbl = new AuthenticationServiceXbl(configuration, httpClientFactory);
        //    //_authServDb = new AuthenticationServiceDb();
        //}

        public AuthenticationService(AuthenticationServiceXbl authServXbl, AuthenticationServiceDb authServDb)
        {
            _authServXbl = authServXbl;
            _authServDb = authServDb;
        }

        // Need authorization_code
        public async Task ZeroStart(string userName, string authorizationCode)
        {
            _authServXbl.OAuthToken = await _authServXbl.RequestOauth2Token(authorizationCode);
            _authServXbl.XauToken = await _authServXbl.RequestXauToken();
            _authServXbl.XstsToken = await _authServXbl.RequestXstsToken();

            await _authServDb.SaveToDb(userName, _authServXbl.XstsToken);
        }
    }
}
