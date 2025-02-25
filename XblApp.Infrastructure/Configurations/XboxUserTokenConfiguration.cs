using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities;

namespace XblApp.Database.Configurations
{
    public class XboxUserTokenConfiguration : IEntityTypeConfiguration<XboxUserToken>
    {
        public void Configure(EntityTypeBuilder<XboxUserToken> builder)
        {
            builder
                .HasKey(x => x.Xuid);
        }
    }
}
