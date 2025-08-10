using System.Linq.Expressions;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IGamerRepository
    {
        public Task<Gamer> GetGamerProfileAsync(long id);
        public Task<Gamer> GetGamerProfileAsync(string gamertag);
        public Task<IEnumerable<TKey>> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, TKey>> selectExpression);
        public Task<Gamer> GetGamesForGamerAsync(string gamertag);
        public Task SaveOrUpdateGamersAsync(GamerJson gamers);
        public Task<bool> IsGamertagLinkedToUserAsync(string gamertag);
    }
}
