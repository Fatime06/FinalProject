using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating>
    {
        public void Configure(EntityTypeBuilder<ProductRating> builder)
        {
            builder.Property(pr => pr.Value)
                .IsRequired();
            builder.Property(c => c.CreatedDate)
              .IsRequired()
              .HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate)
                .HasColumnType("datetime2");
        }
    }
}
