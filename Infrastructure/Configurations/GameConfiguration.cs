using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Infrastructure.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            // Указываем ключ и отключаем автоинкремент
            builder.HasKey(g => g.GameId);
            builder.Property(g => g.GameId)
                .ValueGeneratedNever(); // Аналог DatabaseGeneratedOption.None

            // GameName (обязательное поле, не может быть пустым)
            builder.Property(g => g.GameName)
                .IsRequired()
                .HasMaxLength(200); // Можно ограничить длину строки

            // TotalAchievements (обязательное поле)
            builder.Property(g => g.TotalAchievements)
                .IsRequired();

            // TotalGamerscore (обязательное поле)
            builder.Property(g => g.TotalGamerscore)
                .IsRequired();

            // Один ко многим: Game <-> Achievement
            builder.HasMany(g => g.AchievementLinks)
                .WithOne(gg => gg.GameLink)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один ко многим: Game <-> GamerGame
            builder.HasMany(g => g.GamerGameLinks)
                .WithOne(gg => gg.GameLink)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один ко многим: Game <-> GamerAchievement
            builder.HasMany(g => g.GamerAchievementLinks)
                .WithOne(gg => gg.GameLink)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.NoAction);//Удаление записей в GamerAchievement настроено в AchievementConfiguration
        }
    }
}
