using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Product;

namespace Service.AutoMappers
{
    public class ProductAutoMapper : Profile
    {
        public ProductAutoMapper()
        {
            CreateMap<ProductCreateVM, Product>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<ProductUpdateVM, Product>().ReverseMap()
                    .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Product, ProductVM>();
            CreateMap<Category, CategoryInProductVM>();
        }
    }
}
