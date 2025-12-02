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
            CreateMap<Product, ProductVM>()
                 .ForMember(dest => dest.AverageRating,
               opt => opt.MapFrom(src => src.Ratings.Any()
                                  ? src.Ratings.Average(r => r.Value)
                                  : 0))
                 .ForMember(dest => dest.Ratings,opt=> opt.MapFrom(src => src.Ratings))
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<Category, CategoryInProductVM>();
        }
    }
}
