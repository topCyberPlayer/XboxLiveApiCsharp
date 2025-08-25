using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class GamerAchievementConfiguration : IEntityTypeConfiguration<GamerAchievement>
    {
        public void Configure(EntityTypeBuilder<GamerAchievement> builder)
        {
            // Композитный первичный ключ (GamerId + GameId + AchievementId)
            builder.HasKey(ga => new { ga.GamerId, ga.GameId, ga.AchievementId });

            // Дата получения достижения
            builder.Property(ga => ga.DateUnlocked)
                .HasColumnType("datetime2");

            // Флаг получения достижения
            builder.Property(ga => ga.IsUnlocked)
                .IsRequired();
        }
    }
}
