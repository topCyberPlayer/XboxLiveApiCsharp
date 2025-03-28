using XblApp.Domain.Entities;
using XblApp.Domain.JsonModels;

namespace XblApp.Domain.Interfaces.IRepository
{
    public interface IAchievementRepository
    {
        public Task<List<Achievement?>> GetAllAchievementsAsync();
        public Task<List<GamerAchievement>> GetGamerAchievementsAsync(long xuid);
        public Task<List<Achievement>> GetAchievementsAsync(string gameName);
        public Task SaveAchievementsAsync(AchievementJson achievements);
    }
}
