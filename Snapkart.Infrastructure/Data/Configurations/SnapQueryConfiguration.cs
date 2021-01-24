using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snapkart.Domain.Entities;

namespace Snapkart.Infrastructure.Data.Configurations
{
    public class SnapQueryConfiguration : IEntityTypeConfiguration<SnapQuery>
    {
        public void Configure(EntityTypeBuilder<SnapQuery> builder)
        {
            builder.HasMany(x => x.Bids)
                .WithOne(x => x.SnapQuery)
                .HasForeignKey(x => x.SnapQueryId);
        }
    }
}