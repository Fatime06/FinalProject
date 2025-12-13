using AutoMapper;
using Domain.Entities;
using Service.ViewModels.BasketItem;

namespace Service.AutoMappers
{
    public class BasketItemAutoMapper : Profile
    {
        public BasketItemAutoMapper()
        {
            CreateMap<BasketItem, BasketItemVM>()
                  .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                  .ForMember(d => d.ProductImage, o => o.MapFrom(s => s.Product.Image));
        }
    }
}
