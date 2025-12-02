using AutoMapper;
using Domain.Entities;
using Service.DTOs.Category;

namespace Service.AutoMappers
{
    public class CategoryAutoMapper : Profile
    {
        public CategoryAutoMapper()
        {
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryUpdateVM, Category>().ReverseMap();
            CreateMap<Category, CategoryVM>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        }
    }
}
