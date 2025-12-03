using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.AppUser)
                   .WithMany(au => au.Reviews)
                   .HasForeignKey(r => r.AppUserId);
            builder.Property(r => r.Rating).IsRequired();
            builder.Property(r => r.Text).IsRequired().HasMaxLength(1000);
            builder.Property(c => c.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
