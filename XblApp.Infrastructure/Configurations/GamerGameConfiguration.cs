﻿using Microsoft.EntityFrameworkCore;
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

            builder.Property(gg => gg.CurrentAchievements)
                .IsRequired();

            builder.Property(gg => gg.CurrentGamerscore)
                .IsRequired();
        }
    }
}
