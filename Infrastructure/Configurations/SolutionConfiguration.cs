using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SolutionConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(s => s.SolutionId);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.ChangedAt)
                .IsRequired(false);

            // Связь с достижением
            builder.HasOne(s => s.AchievementLink)
                .WithMany(a => a.SolutionLinks)
                .HasForeignKey(s => s.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            // Связь с игроком (автор решения)
            builder.HasOne(s => s.GamerLink)//у Solution есть один Gamer.
                .WithMany(g => g.SolutionLinks)//У Gamer может быть много решений
                .HasForeignKey(s => s.GamerId)//Указываем, что внешний ключ (FK) в таблице Solutions будет храниться в поле GamerId.
                                              //То есть в БД появится колонка Solutions.GamerId, которая указывает на Gamers.Id.
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
