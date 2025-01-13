using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using XblApp.Domain.Entities;

namespace XblApp.Infrastructure.Data.Contexts
{
    public class TotalAchievementsInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;

            // Обновляем TotalAchievements для всех изменённых игр
            var games = context.ChangeTracker
                .Entries<Game>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (EntityEntry<Game> gameEntry in games)
            {
                Game game = gameEntry.Entity;

                // Подсчитываем достижения, связанные с текущей игрой
                game.TotalAchievements = game.Achievements.Count;
                game.TotalGamerscore = game.Achievements.Sum(a => a.Gamerscore);
            }

            return base.SavingChanges(eventData, result);
        }
    }
}
