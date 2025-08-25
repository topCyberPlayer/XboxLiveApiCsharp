using Domain.Entities;
using Domain.Entities.JsonModels;
using System.Linq.Expressions;

namespace Domain.Interfaces.IRepository
{
    public interface IAchievementRepository
    {
        Task<IEnumerable<TKey?>> GetAllAchievementsAsync<TKey>(
            Expression<Func<Achievement, TKey>> selectExpression);
        Task<IEnumerable<TKey?>> GetAchievementsAsync<TKey>(
            Expression<Func<Achievement, bool>> filterExpression,
            Expression<Func<Achievement, TKey>> selectExpression);//gamename
        Task SaveAchievementsAsync(AchievementX1Json achievements);
        Task SaveAchievementsAsync(AchievementX360Json achievementsX360);
    }
}
