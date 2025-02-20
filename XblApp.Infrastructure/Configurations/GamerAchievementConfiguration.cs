using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class GamerAchievementConfiguration : IEntityTypeConfiguration<GamerAchievement>
    {
        public void Configure(EntityTypeBuilder<GamerAchievement> builder)
        {
            // Композитный первичный ключ (GamerId + AchievementId)
            builder.HasKey(ga => new { ga.GamerId, ga.AchievementId });

            // Связь с Gamer (многие ко многим)
            builder.HasOne(ga => ga.GamerLink)
                .WithMany(g => g.AchievementLinks)
                .HasForeignKey(ga => ga.GamerId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление достижений при удалении игрока

            // Связь с Achievement (многие ко многим)
            builder.HasOne(ga => ga.AchievementLink)
                .WithMany(a => a.GamerLinks)
                .HasForeignKey(ga => ga.AchievementId)
                .OnDelete(DeleteBehavior.Cascade); // Удаление связки при удалении достижения

            // Дата получения достижения
            builder.Property(ga => ga.DateUnlocked)
                .IsRequired();
        }
    }
}
