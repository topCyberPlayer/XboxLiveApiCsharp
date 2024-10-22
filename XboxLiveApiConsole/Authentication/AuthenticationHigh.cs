using ConsoleApp.SaveResponses;
using ConsoleApp.Storages;

namespace ConsoleApp.Authentication
{
    internal class AuthenticationHigh
    {
        private AuthenticationLow _authMgr;
        private IStorage _store;

        private SaveOAuth2 _saveOAuth2;
        private SaveXau _saveXau;
        private SaveXsts _saveXsts;

        public AuthenticationHigh(AuthenticationLow authMgr, IStorage store)
        {
            this._authMgr = authMgr;
            this._store = store;

            this._saveOAuth2 = new SaveOAuth2();
            this._saveXau = new SaveXau();
            this._saveXsts = new SaveXsts();
        }

        public async Task ReadTokens()
        {
            DateTime dateUtcNow = DateTime.UtcNow;

            _authMgr.XstsToken = await _store.GetToken<XSTSResponse>(_saveXsts);

            if (_authMgr.XstsToken != null && _authMgr.XstsToken.NotAfter > dateUtcNow)
                return;
            else 
            {
                if (_authMgr.XstsToken == null)
                {
                    string authorization_code = await _authMgr.GetAuthCodeFromBrowser();

                    await RequestTokens(authorization_code);
                }
                else
                {
                    if (_authMgr.XstsToken.NotAfter < dateUtcNow)
                    {
                        await RefreshTokens();
                    }
                }
            }
        }

        /// <summary>
        /// Request all tokens
        /// </summary>
        /// <returns></returns>
        private async Task RequestTokens(string authorization_code)
        {
            _authMgr.OAuth = await _authMgr.RequestOauth2Token(authorization_code);
            await _store.SaveToken(_saveOAuth2, _authMgr.OAuth);

            _authMgr.UserToken = await _authMgr.RequestXauToken();
            await _store.SaveToken(_saveXau, _authMgr.UserToken);

            _authMgr.XstsToken = await _authMgr.RequestXstsToken();
            await _store.SaveToken(_saveXsts,_authMgr.XstsToken);
        }

        private async Task RefreshTokens()
        {
            _authMgr.OAuth = await _authMgr.RefreshOauth2Token();
            await _store.SaveToken(_saveOAuth2, _authMgr.OAuth);

            _authMgr.UserToken = await _authMgr.RequestXauToken();
            await _store.SaveToken(_saveXau, _authMgr.UserToken);

            _authMgr.XstsToken = await _authMgr.RequestXstsToken();
            await _store.SaveToken(_saveXsts, _authMgr.XstsToken);
        }
    }

    public sealed class Country
    {
        public readonly int[] m_Values;
        public readonly string m_Name;

        private Country(string name, int[] codes) 
        { 
            this.m_Name = name;
            this.m_Values = codes;
        }

        public static Country US = new Country("United States", new[] { 1, 2 });
        public static Country Canada = new Country("Canada", new[] { 3, 4 });
    }
}
