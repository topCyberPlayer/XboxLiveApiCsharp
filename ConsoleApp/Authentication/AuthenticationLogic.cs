using ConsoleApp.Store;
using System.Diagnostics;
using System.Net;

namespace ConsoleApp.Authentication
{
    internal class AuthenticationLogic
    {
        private AuthenticationManager _authMgr;
        private string _redirectUri;
        private IStorage _store;

        public AuthenticationLogic(AuthenticationManager authMgr, IStorage store)
        {
            this._authMgr = authMgr;
            this._redirectUri = authMgr.redirectUrl;
            this._store = store;
        }

        public async Task Start()
        {
            DateTime dateUtcNow = DateTime.UtcNow;
            _authMgr.XstsToken = await _store.GetToken<XSTSResponse>();

            if(_authMgr.XstsToken != null && _authMgr.XstsToken.NotAfter < dateUtcNow)
            {
                //Обновить токены
                _authMgr.OAuth = await _store.GetToken<OAuth2TokenResponse>();

                if (_authMgr.OAuth != null)
                {
                    if (true)//_authMgr.OAuth.Expires < dateUtcNow)
                    {
                        _authMgr.OAuth = await _authMgr.RefreshOauth2Token();
                        await _store.SaveToken(_authMgr.OAuth);
                    }

                    _authMgr.UserToken = await _store.GetToken<XAUResponse>();
                    if (_authMgr.UserToken.NotAfter < dateUtcNow)
                    {
                        _authMgr.UserToken = await _authMgr.RequestXauToken();
                        await _store.SaveToken(_authMgr.UserToken);
                    }

                    _authMgr.XstsToken = await _store.GetToken<XSTSResponse>();
                    if (_authMgr.XstsToken.NotAfter < dateUtcNow)
                    {
                        _authMgr.XstsToken = await _authMgr.RequestXstsToken();
                        await _store.SaveToken(_authMgr.XstsToken);
                    }
                }
                else
                {
                    string authorization_code = await GetAuthCodeFromBrowser();

                    await RequestTokens(authorization_code);
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
            _authMgr.UserToken = await _authMgr.RequestXauToken();
            _authMgr.XstsToken = await _authMgr.RequestXstsToken();

            await _store.SaveToken(_authMgr.OAuth);
            await _store.SaveToken(_authMgr.UserToken);
            await _store.SaveToken(_authMgr.XstsToken);
        }

        /// <summary>
        /// Авторизация через браузер. Результат работы - authorization_code, который получаем из браузера и подставляем в Request_tokens
        /// </summary>
        /// <param name="_auth_mgr"></param>
        /// <returns></returns>
        private async ValueTask<string> GetAuthCodeFromBrowser()
        {
            string auth_url = _authMgr.GenerateAuthorizationUrl();

            using (HttpListener http = new HttpListener())
            {
                http.Prefixes.Add(_redirectUri + "/");
                http.Start();

                Process.Start(new ProcessStartInfo(auth_url) { UseShellExecute = true });

                string authorization_code = await HandleOAuth2Redirect(http);

                return authorization_code;
            }
        }

        /// <summary>
        /// Возвращает authorization_code, который получаем из браузера
        /// </summary>
        /// <param name="http"></param>
        /// <returns></returns>
        private async ValueTask<string> HandleOAuth2Redirect(HttpListener http)
        {
            var context = await http.GetContextAsync();

            Uri uri = context.Request.Url;

            context.Response.OutputStream.Close();

            string code = uri.Query.Substring(uri.Query.IndexOf("=") + 1);

            return code;
        }

        public async Task GetFromASaveToB(IStorage storageFrom, IStorage storageTo)
        {
            //OAuth2TokenResponse fromOAuth2Token = await storageFrom.GetToken<OAuth2TokenResponse>();
            //await storageTo.SaveToken(fromOAuth2Token);

            XSTSResponse fromXSTS = await storageFrom.GetToken<XSTSResponse>();            
            await storageTo.SaveToken(fromXSTS);
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
