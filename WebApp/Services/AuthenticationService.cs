namespace WebApp.Services
{
    public class AuthenticationService
    {
        private AuthenticationServiceXbl _authServXbl;
        private AuthenticationServiceDb _authServDb;

        public AuthenticationService(AuthenticationServiceXbl authServXbl, AuthenticationServiceDb authServDb)
        {
            _authServXbl = authServXbl;
            _authServDb = authServDb;
        }

        /// <summary>
        /// Need authorizationCode
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="authorizationCode"></param>
        /// <returns></returns>
        public async Task ZeroStart(string userName, string authorizationCode)
        {
            _authServXbl.OAuthToken = await _authServXbl.RequestOauth2Token(authorizationCode);
            _authServXbl.XauToken = await _authServXbl.RequestXauToken();
            _authServXbl.XstsToken = await _authServXbl.RequestXstsToken();

            _authServDb.SaveToDb(userName, _authServXbl.XstsToken);
        }
    }
}
