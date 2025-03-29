using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class GamerAchievementConfiguration : IEntityTypeConfiguration<GamerAchievement>
    {
        public void Configure(EntityTypeBuilder<GamerAchievement> builder)
        {
            // Композитный первичный ключ (GamerId + GameId + AchievementId)
            builder.HasKey(ga => new { ga.GamerId, ga.GameId, ga.AchievementId });

            // Связь с Gamer (многие ко многим)
            builder.HasOne(ga => ga.GamerLink)
                .WithMany(g => g.GamerAchievementLinks)
                .HasForeignKey(ga => ga.GamerId)
                .OnDelete(DeleteBehavior.Cascade); // Если удалить игрока (Gamer), то все связанные GamerAchievement записи тоже будут удалены.

            builder.HasOne(ga => ga.GameLink)
                .WithMany(g => g.GamerAchievementLinks)
                .HasForeignKey(ga => ga.GameId)
                .OnDelete(DeleteBehavior.Restrict); // Если удалить игру (Game), то все связанные GamerAchievement записи не удалятся

            // Связь с Achievement (многие ко многим)
            builder.HasOne(ga => ga.AchievementLink)
                .WithMany(a => a.GamerAchievementLinks)
                .HasForeignKey(ga => new { ga.AchievementId, ga.GameId })
                .OnDelete(DeleteBehavior.Cascade); // Если удалить достижение (Achievement), то все связанные GamerAchievement записи также удалятся.

            // Дата получения достижения
            builder.Property(ga => ga.DateUnlocked)
                .HasColumnType("datetime2");

            // Флаг получения достижения
            builder.Property(ga => ga.IsUnlocked)
                .IsRequired();
        }
    }
}
