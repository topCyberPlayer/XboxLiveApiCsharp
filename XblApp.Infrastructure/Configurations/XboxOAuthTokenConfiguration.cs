using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    internal class XboxOAuthTokenConfiguration : IEntityTypeConfiguration<XboxOAuthToken>
    {
        public void Configure(EntityTypeBuilder<XboxOAuthToken> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.UserId);

            modelBuilder
                .HasOne(x => x.XboxLiveTokenLink)
                .WithOne(x => x.XboxOAuthTokenLink)
                .HasForeignKey<XboxLiveToken>(x => x.UserIdFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
