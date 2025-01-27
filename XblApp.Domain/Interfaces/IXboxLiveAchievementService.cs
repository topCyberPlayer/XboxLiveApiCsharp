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

        /// <summary>
        /// Get recent achievement progress and information
        /// </summary>
        /// <param name="xuid">Xbox User Id</param>
        /// <returns>Achievement 360 Response</returns>
        public Task<List<Achievement>> GetAchievementsXbox360RecentProgressAndInfoAsync(long xuid);
    }
}
