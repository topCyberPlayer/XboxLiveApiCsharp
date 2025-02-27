using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Database.Contexts;
using XblApp.Domain.Entities;
using XblApp.Domain.Interfaces;

namespace XblApp.Database.Repositories
{
    public class AchievementRepository : BaseRepository, IAchievementRepository
    {
        public AchievementRepository(XblAppDbContext context) : base(context)
        {
        }

        public Task<Achievement> GetAchievementsAsync(string gameName)
        {
            throw new NotImplementedException();
        }

        public Task<List<Achievement>> GetAllAchievementsAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdateAchievementsAsync(List<Achievement> achievements)
        {
            throw new NotImplementedException();
        }
    }
}
