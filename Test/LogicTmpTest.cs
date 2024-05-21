using DataLayer.Logic;
using ServiceLayer.Models;

namespace Test
{
    public class LogicTmpTest
    {
        LogicTmp a;
        public LogicTmpTest()
        {
            a = new LogicTmp();
        }

        [Fact]
        public void Test1()
        {
            var result = a.FindByGamertag("GamerOne");
        }

        [Fact]
        public void Test2()
        {
            List<GamerModelDto> result = a.GetAllGamers().ToList();
        }
    }
}