using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(au => au.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(au => au.Surname)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(au=>au.Address)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(au => au.UserName)
                .IsRequired()
                .HasMaxLength(100);
            
        }
    }
}
