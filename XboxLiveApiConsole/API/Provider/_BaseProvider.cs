using ConsoleApp.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.API.Provider
{
    internal class BaseProvider
    {
        private protected AuthenticationLow _authMgr;

        public BaseProvider(AuthenticationLow authMgr)
        {
            this._authMgr = authMgr;
        }
    }
}
