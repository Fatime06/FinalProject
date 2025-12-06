using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(c => c.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
