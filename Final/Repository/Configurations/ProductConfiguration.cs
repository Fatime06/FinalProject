using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(35);
            builder.Property(p => p.Image)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.InStock)
                .IsRequired();
            builder.Property(p => p.Quantity)
                .IsRequired();
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate)
                .HasColumnType("datetime2");
            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
