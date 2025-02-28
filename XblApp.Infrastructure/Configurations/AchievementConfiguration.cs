using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            // Указываем ключ и отключаем автоинкремент
            builder.HasKey(a => a.AchievementId);
            builder.Property(a => a.AchievementId)
                .ValueGeneratedNever(); // Аналог DatabaseGeneratedOption.None

            // Внешний ключ к Game
            builder.HasOne(a => a.GameLink)
                .WithMany(g => g.AchievementLinks)
                .HasForeignKey(a => a.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Name (обязательное поле, не может быть пустым)
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150); // Можно добавить ограничение длины

            // Description (обязательное поле, не может быть пустым)
            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);

            // Gamerscore (обязательное поле)
            builder.Property(a => a.Gamerscore)
                .IsRequired();

            // IsSecret (обязательное поле)
            builder.Property(a => a.IsSecret)
                .IsRequired();

            // Один ко многим: Achievement <-> GamerAchievement (многие ко многим через промежуточную таблицу)
            builder.HasMany(a => a.GamerLinks)
                .WithOne()
                .HasForeignKey(ga => ga.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
