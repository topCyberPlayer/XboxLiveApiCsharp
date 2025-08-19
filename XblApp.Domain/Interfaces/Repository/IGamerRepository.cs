using System.Linq.Expressions;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IGamerRepository
    {
        Task<TKey> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, bool>> filterExpression,
            Expression<Func<Gamer, TKey>> selectExpression);
        
        Task<IEnumerable<TKey>> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, TKey>> selectExpression);
        
        Task SaveOrUpdateGamersAsync(GamerJson gamers);
        
        Task<bool> IsGamertagLinkedToUserAsync(string gamertag);
        
    }
}
