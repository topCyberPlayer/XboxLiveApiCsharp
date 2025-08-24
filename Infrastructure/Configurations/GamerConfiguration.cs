using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;
using XblApp.Infrastructure.Models;

namespace XblApp.Infrastructure.Configurations
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
            builder.HasMany(g => g.GamerGameLinks)//Говорит, что у Gamer может быть много записей в GamerGame
                .WithOne(gg => gg.GamerLink)//Говорит, что у GamerGame есть только одна связь с Gamer
                .HasForeignKey(gg => gg.GamerId)//Говорит, что внешний ключ для этой связи — GamerId в GamerGame
                .OnDelete(DeleteBehavior.Cascade);//Если удалить Gamer, все его GamerGame связи тоже удаляются

            // Один ко многим: Gamer <-> GamerAchievement
            builder.HasMany(g => g.GamerAchievementLinks)
                .WithOne(gg => gg.GamerLink)
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
