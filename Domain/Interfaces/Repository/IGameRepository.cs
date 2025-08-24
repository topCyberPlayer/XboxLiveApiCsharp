using System.Linq.Expressions;
using XblApp.Domain.Entities;
using XblApp.Domain.Entities.JsonModels;

namespace XblApp.Domain.Interfaces.Repository
{
    public interface IGameRepository
    {
        public Task<TKey> GetInclude_GamerGame_Achievement_GamerAchievement_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression,
            Expression<Func<Game, bool>> filterExpression);

        public Task<IEnumerable<TKey>> GetInclude_GamerGameLinks_Async<TKey>(
            Expression<Func<Game, TKey>> selectExpression);

        /// <summary>
        /// Сохранение происходит в 2 таблицы: Game и GamerGame
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        public Task SaveOrUpdateGamesAsync(GameJson games);
    }
}
