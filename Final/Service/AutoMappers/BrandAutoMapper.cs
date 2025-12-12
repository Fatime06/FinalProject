using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Brand;

namespace Service.AutoMappers
{
    public class BrandAutoMapper : Profile
    {
        public BrandAutoMapper()
        {
            CreateMap<BrandCreateVM, Brand>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<BrandUpdateVM, Brand>().ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Brand, BrandVM>();
            CreateMap<Product, ProductInBrandVM>();
        }
    }
}
