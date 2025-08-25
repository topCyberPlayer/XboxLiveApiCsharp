using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            // Композитный первичный ключ (AchievementId + GameId)
            builder.HasKey(a => new { a.AchievementId, a.GameId });

            // Name (обязательное поле, не может быть пустым)
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150); // Можно добавить ограничение длины

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.Gamerscore)
                .IsRequired();

            builder.Property(a => a.IsSecret)
                .IsRequired();

            // Один ко многим: Achievement <-> GamerAchievement
            builder.HasMany(a => a.GamerAchievementLinks)
                .WithOne(gg => gg.AchievementLink)
                .HasForeignKey(ga => new { ga.AchievementId, ga.GameId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
