using WebApp.Services.Authentication;

namespace WebApp.Services
{
    internal class BaseLow
    {
        private protected AuthenticationLow _authMgr;

        public BaseLow(AuthenticationLow authMgr)
        {
            this._authMgr = authMgr;
        }
    }
}
