using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Database.Models;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class GamerConfiguration : IEntityTypeConfiguration<Gamer>
    {
        public void Configure(EntityTypeBuilder<Gamer> builder)
        {
            // Указываем ключ и отключаем автоинкремент
            builder.HasKey(g => g.GamerId);
            builder.Property(g => g.GamerId)
                .ValueGeneratedNever(); // Аналог DatabaseGeneratedOption.None

            // Gamertag (обязательный, не может быть пустым)
            builder.Property(g => g.Gamertag)
                .IsRequired()
                .HasMaxLength(100); // Можно добавить ограничение длины

            // Gamerscore (обязательное поле)
            builder.Property(g => g.Gamerscore)
                .IsRequired();

            // Bio и Location (необязательные поля)
            builder.Property(g => g.Bio)
                .HasMaxLength(500); // Можно ограничить длину строки
            builder.Property(g => g.Location)
                .HasMaxLength(100);

            // Один ко многим: Gamer <-> GamerGame
            builder.HasMany(g => g.GamerGameLinks)
                .WithOne()
                .HasForeignKey(gg => gg.GamerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Один ко многим: Gamer <-> GamerAchievement
            builder.HasMany(g => g.GamerAchievementLinks)
                .WithOne()
                .HasForeignKey(ga => ga.GamerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь 1 к 1 с ApplicationUser
            builder.HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Gamer>(g => g.ApplicationUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
