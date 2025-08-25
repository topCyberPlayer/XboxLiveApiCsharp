using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.XblAuth;

namespace Infrastructure.Configurations
{
    public class XboxLiveTokenConfiguration : IEntityTypeConfiguration<XboxXauToken>
    {
        public void Configure(EntityTypeBuilder<XboxXauToken> modelBuilder)
        {
            modelBuilder.
                HasKey(x => x.UhsId);

            modelBuilder
                .HasOne(x => x.XboxXstsTokenLink)
                .WithOne(x => x.XboxXauTokenLink)
                .HasForeignKey<XboxXstsToken>(x => x.UhsIdFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
