using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(s => s.Icon)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(s => s.Text)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
