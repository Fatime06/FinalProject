using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Message)
                   .IsRequired()
                   .HasMaxLength(500);
            builder.HasOne(c => c.AppUser)
                 .WithMany()
                 .HasForeignKey(c => c.AppUserId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.Property(c => c.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
