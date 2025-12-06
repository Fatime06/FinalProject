using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(bt => new { bt.BlogId, bt.TagId });

            builder.HasOne(bt => bt.Blog)
                  .WithMany(b => b.BlogTags)
                  .HasForeignKey(bt => bt.BlogId);

            builder.HasOne(bt => bt.Tag)
                  .WithMany(t => t.BlogTags)
                  .HasForeignKey(bt => bt.TagId);
        }
    }
}
