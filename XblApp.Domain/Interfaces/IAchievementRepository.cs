using XblApp.Domain.Entities;

namespace XblApp.Domain.Interfaces
{
    public interface IAchievementRepository
    {
        public Task SaveAchievementsAsync(List<Achievement> achievements);

        public Task<List<Achievement>> GetAllAchievementsAsync();

        public Task<Achievement> GetAchievementsAsync(string gameName);
    }
}
