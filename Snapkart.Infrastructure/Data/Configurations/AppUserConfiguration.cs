using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snapkart.Domain.Constants;
using Snapkart.Domain.Entities;

namespace Snapkart.Infrastructure.Data.Configurations
{
    public class AppUserConfiguration: IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            
            builder.Property(x => x.ApprovalStatus)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                    x => (ApprovalStatus) Enum.Parse(typeof(ApprovalStatus), x));
            
            builder.Property(x => x.Role)
                .IsRequired()
                .HasConversion(x => x.ToString(),
                    x => (UserRole) Enum.Parse(typeof(UserRole), x));
        }
    }
}