using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAchievementRepository
    {
        public Task<List<Achievement>> GetAllAchievementsAsync();
        public Task<Achievement> GetAchievementsAsync(string gameName);
        public Task SaveOrUpdateAchievementsAsync(List<Achievement> achievements);
        public Task SaveOrUpdateGamerAchievementsAsync(List<GamerAchievement> gamerAchievements);
    }
}
