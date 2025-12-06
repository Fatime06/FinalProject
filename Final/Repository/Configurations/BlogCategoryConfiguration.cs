using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.HasKey(bc => new { bc.BlogId, bc.CategoryId });

            builder.HasOne(bc => bc.Blog)
                  .WithMany(b => b.BlogCategories)
                  .HasForeignKey(bc => bc.BlogId);

            builder.HasOne(bc => bc.Category)
                  .WithMany(c => c.BlogCategories)
                  .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
