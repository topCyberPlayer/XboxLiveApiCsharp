using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class XboxLiveTokenConfiguration : IEntityTypeConfiguration<XboxLiveToken>
    {
        public void Configure(EntityTypeBuilder<XboxLiveToken> modelBuilder)
        {
            modelBuilder.
                HasKey(x => x.UhsId);

            modelBuilder
                .HasOne(x => x.UserTokenLink)
                .WithOne(x => x.XboxLiveToken)
                .HasForeignKey<XboxUserToken>(x => x.UhsIdFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
