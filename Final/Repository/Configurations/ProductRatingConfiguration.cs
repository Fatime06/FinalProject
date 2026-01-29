using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

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

            builder.HasOne(pr => pr.Order).WithMany().HasForeignKey(pr => pr.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(pr => pr.Product).WithMany(p => p.Ratings)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
