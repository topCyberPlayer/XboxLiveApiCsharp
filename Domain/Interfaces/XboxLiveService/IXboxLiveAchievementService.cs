namespace Domain.Interfaces.XboxLiveService
{
    public interface IXboxLiveAchievementService<T> where T : class
    {
        /// <summary>
        /// Возвращает все достижения для игрока.
        /// Для игр X1 возвращает все достижения: разблокированные и заблокированные (для каждого указывая статус)
        /// Для игр X360 возвращает только разблокированные достижения
        /// </summary>
        /// <param name="xuid"></param>
        /// <returns></returns>
        public Task<T> GetAllAchievementsForGamerAsync(long xuid);

        /// <summary>
        /// Возвращает все достижения для игры, с указанием разблокировано достижение для игрока или нет
        /// Для игр X1 возвращает все достижения.
        /// Для игр X360 возвращает только разблокированные достижения
        /// </summary>
        /// <param name="xuid">ID игрока</param>
        /// <param name="titleId">ID игры</param>
        /// <returns></returns>
        public Task<T> GetAchievementsForOneGameAsync(long xuid, long titleId);
    }
}
