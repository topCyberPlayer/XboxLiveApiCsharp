using WebApp.Services.Authentication;

namespace WebApp.Services
{
    public class BaseLow
    {
        private protected AuthenticationProviderJson _authMgr;

        public BaseLow(AuthenticationProviderJson authMgr)
        {
            this._authMgr = authMgr;
        }
    }
}
