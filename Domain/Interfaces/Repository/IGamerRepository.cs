using Domain.Entities;
using Domain.Entities.JsonModels;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IGamerRepository
    {
        Task<IEnumerable<TKey>> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, TKey>> selectExpression);

        Task<TKey> GetInclude_GamerGame_Game_Async<TKey>(
            Expression<Func<Gamer, bool>> filterExpression,
            Expression<Func<Gamer, TKey>> selectExpression);

        Task<TKey> GetInclude_GamerAchievement_Achievement_Async<TKey>(
            Expression<Func<Gamer, bool>> filterExpression,
            Expression<Func<Gamer, TKey>> selectExpression);

        Task SaveOrUpdateGamersAsync(GamerJson gamers);

        Task<bool> IsGamertagLinkedToUserAsync(string gamertag);
    }
}
