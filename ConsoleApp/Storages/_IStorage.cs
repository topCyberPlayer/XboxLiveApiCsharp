using ConsoleApp.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Store
{
    internal interface IStorage
    {
        public Task SaveToken<T>(T value);

        public Task<T> GetToken<T>();
    }
}
