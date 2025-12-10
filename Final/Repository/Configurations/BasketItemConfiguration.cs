using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        }
    }
}
