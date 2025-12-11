using AutoMapper;
using Domain.Entities;
using Service.DTOs.Category;

namespace Service.AutoMappers
{
    public class CategoryAutoMapper : Profile
    {
        public CategoryAutoMapper()
        {
            CreateMap<CategoryCreateVM, Category>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<CategoryUpdateVM, Category>().ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        }
    }
}
