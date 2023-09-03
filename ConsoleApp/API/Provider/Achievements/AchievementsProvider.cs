using ConsoleApp.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.API.Provider.Achievements
{
    internal class AchievementsProvider : BaseProvider
    {

        public AchievementsProvider(AuthenticationManager authMgr) : base(authMgr)
        {
        }
    }

    public class AchievementsResponse
    {

    }
}
