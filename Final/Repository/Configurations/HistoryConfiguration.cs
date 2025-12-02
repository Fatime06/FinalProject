using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.Property(h => h.Year).IsRequired();
            builder.Property(h => h.Title).IsRequired().HasMaxLength(50);
            builder.Property(h => h.Text).IsRequired().HasMaxLength(200);
            builder.Property(c => c.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
