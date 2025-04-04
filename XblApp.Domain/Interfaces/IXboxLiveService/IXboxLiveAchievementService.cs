namespace XblApp.Domain.Interfaces.IXboxLiveService
{
    public interface IXboxLiveAchievementService<T>
    {
        /// <summary>
        /// Возвращает все достижения для игрока
        /// </summary>
        /// <param name="xuid"></param>
        /// <returns></returns>
        public Task<T> GetAchievementsAsync(long xuid);

        /// <summary>
        /// Возвращает все достижения для игры, с указанием разблокировано достижение для игрока или нет
        /// </summary>
        /// <param name="xuid">ID игрока</param>
        /// <param name="titleId">ID игры</param>
        /// <returns></returns>
        public Task<T> GetAchievementsAsync(long xuid, long titleId);
    }
}
