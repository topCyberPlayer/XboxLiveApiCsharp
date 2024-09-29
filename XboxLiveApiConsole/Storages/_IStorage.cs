using ConsoleApp.SaveResponses;

namespace ConsoleApp.Storages
{
    internal interface IStorage
    {
        public Task<T> GetToken<T>(SaveResponse saveResponse);

        public Task SaveToken<T>(SaveResponse saveResponse, T value);

        
    }
}
