using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities.XblAuth;

namespace XblApp.Infrastructure.Configurations
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
