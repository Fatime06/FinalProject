using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(s => s.Image).IsRequired().HasMaxLength(100);
            builder.Property(s => s.SmallText).IsRequired().HasMaxLength(25);
            builder.Property(s => s.BigText).IsRequired().HasMaxLength(45);
            builder.Property(s => s.ButtonText).IsRequired().HasMaxLength(20);
            builder.Property(s => s.MediumText).IsRequired().HasMaxLength(100);
            builder.Property(s => s.SmallNote).HasMaxLength(50);
            builder.Property(c => c.CreatedDate).IsRequired().HasColumnType("datetime2");
            builder.Property(c => c.UpdatedDate).HasColumnType("datetime2");
        }
    }
}
