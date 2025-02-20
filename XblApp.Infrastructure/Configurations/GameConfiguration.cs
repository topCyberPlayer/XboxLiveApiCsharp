using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
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
                .WithOne()
                .HasForeignKey(a => a.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один ко многим: Game <-> GamerGame (многие ко многим через промежуточную таблицу)
            builder.HasMany(g => g.GamerLinks)
                .WithOne()
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
