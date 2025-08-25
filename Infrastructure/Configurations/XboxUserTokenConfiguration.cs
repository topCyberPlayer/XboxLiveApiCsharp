using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities.XblAuth;

namespace Infrastructure.Configurations
{
    public class XboxUserTokenConfiguration : IEntityTypeConfiguration<XboxXstsToken>
    {
        public void Configure(EntityTypeBuilder<XboxXstsToken> builder)
        {
            builder
                .HasKey(x => x.Xuid);
        }
    }
}
