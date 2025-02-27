using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IXboxLiveAchievementService
    {
        /// <summary>
        /// Get recent achievement progress and information
        /// </summary>
        /// <param name="xuid">Xbox User Id</param>
        /// <returns>Recent Xbox One Progress Response</returns>
        public Task<List<Achievement>> GetAchievementsXboxoneRecentProgressAndInfoAsync(long xuid);

        public Task<List<Achievement>> GetAchievements(long xuid);

        public Task<List<Achievement>> GetAchievementsX1Gameprogress(long xuid, long titleId);
    }
}
