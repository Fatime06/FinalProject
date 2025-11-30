using AutoMapper;
using Domain.Entities;
using Service.DTOs.Category;

namespace Service.AutoMappers
{
    public class CategoryAutoMapper : Profile
    {
        public CategoryAutoMapper()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<Category, CategoryDto>();
        }
    }
}
