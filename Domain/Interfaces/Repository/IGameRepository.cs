using Domain.Entities;
using Domain.Entities.JsonModels;
using Domain.Requests;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IGameRepository
    {
        Task<TKey> GetInclude_GamerGame_Achievement_GamerAchievement_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression,
            Expression<Func<Game, bool>> filterExpression);

        Task<IEnumerable<TKey>> GetInclude_GamerGameLinks_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression);

        /// <summary>
        /// Сохранение происходит в 2 таблицы: Game и GamerGame
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        Task SaveOrUpdateGamesAsync(GameJson games);
        Task SaveOrUpdateGamesAsync(GameRequest request);
    }
}
