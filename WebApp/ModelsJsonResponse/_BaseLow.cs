using ModelsJsonResponse.Authentication;

namespace ModelsJsonResponse
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
