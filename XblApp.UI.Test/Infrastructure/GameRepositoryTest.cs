using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Infrastructure.Data.Repositories;
using XblApp.Test;

namespace XblApp.UI.Test.Infrastructure
{
    
    public class GameRepositoryTest : BaseTestClass
    {
        private GameRepository _gr;

        public GameRepositoryTest()
        {
            _gr = new GameRepository();
        }

        [Fact]
        public void SaveGamesAsync_Test()
        {
            _gr.SaveGamesAsync();
        }
    }
}
