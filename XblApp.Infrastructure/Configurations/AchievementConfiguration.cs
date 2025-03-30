using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
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

            // Один к одному: Achievement <-> Game
            builder.HasOne(a => a.GameLink)
                .WithMany(g => g.AchievementLinks)
                .HasForeignKey(a => a.GameId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
