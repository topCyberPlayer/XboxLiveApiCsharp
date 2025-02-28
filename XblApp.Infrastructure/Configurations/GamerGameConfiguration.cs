using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class GamerGameConfiguration : IEntityTypeConfiguration<GamerGame>
    {
        public void Configure(EntityTypeBuilder<GamerGame> builder)
        {
            // Композитный первичный ключ (GamerId + GameId)
            builder.HasKey(gg => new { gg.GamerId, gg.GameId });

            // Связь с Gamer (многие ко многим)
            builder.HasOne(gg => gg.GamerLink)
                .WithMany(g => g.GamerGameLinks)
                .HasForeignKey(gg => gg.GamerId)
                .OnDelete(DeleteBehavior.Cascade); // Если игрок удаляется, связи удаляются

            // Связь с Game (многие ко многим)
            builder.HasOne(gg => gg.GameLink)
                .WithMany(g => g.GamerGameLinks)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Если игра удаляется, связи удаляются

            // Поля прогресса игрока в игре
            builder.Property(gg => gg.CurrentAchievements)
                .IsRequired();
            builder.Property(gg => gg.CurrentGamerscore)
                .IsRequired();
        }
    }
}
