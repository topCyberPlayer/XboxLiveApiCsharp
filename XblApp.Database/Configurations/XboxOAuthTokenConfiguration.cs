using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XblApp.Domain.Entities.XblAuth;

namespace XblApp.Database.Configurations
{
    internal class XboxOAuthTokenConfiguration : IEntityTypeConfiguration<XboxAuthToken>
    {
        public void Configure(EntityTypeBuilder<XboxAuthToken> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.UserId);

            modelBuilder
                .HasOne(x => x.XboxXauTokenLink)
                .WithOne(x => x.XboxOAuthTokenLink)
                .HasForeignKey<XboxXauToken>(x => x.UserIdFK)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
