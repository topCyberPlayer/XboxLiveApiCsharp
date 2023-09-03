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
        private protected AuthenticationManager _authMgr;

        public BaseProvider(AuthenticationManager authMgr)
        {
            this._authMgr = authMgr;
        }
    }
}
