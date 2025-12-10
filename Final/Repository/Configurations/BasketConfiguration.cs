using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasOne(b => b.AppUser)
                   .WithOne(u => u.Basket)
                   .HasForeignKey<Basket>(b => b.AppUserId);

            builder.HasMany(b => b.BasketItems)
                   .WithOne(bi => bi.Basket)
                   .HasForeignKey(bi => bi.BasketId);   
        }
    }
}
