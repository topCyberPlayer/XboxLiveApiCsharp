using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveAchievementService
    {
        //public Task<List<Achievement>> GetAchievementsX1RecentProgressAsync(long xuid);

        /// <summary>
        /// Возвращает все достижения для игрока
        /// </summary>
        /// <param name="xuid"></param>
        /// <returns></returns>
        public Task<List<Achievement>> GetAchievementsAsync(long xuid);

        /// <summary>
        /// Возвращает все достижения для игры, с указанием разблокировано достижение для игрока или нет
        /// </summary>
        /// <param name="xuid">ID игрока</param>
        /// <param name="titleId">ID игры</param>
        /// <returns></returns>
        public Task<(List<Achievement>, List<GamerAchievement>)> GetAchievementsAsync(long xuid, long titleId);
    }
}
