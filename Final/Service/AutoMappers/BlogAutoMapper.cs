using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Blog;

namespace Service.AutoMappers
{
    public class BlogAutoMapper : Profile
    {
        public BlogAutoMapper()
        {
            CreateMap<Blog, BlogVM>()
           .ForMember(dest => dest.CommentCount,
               opt => opt.MapFrom(src => src.Comments.Count()));

            CreateMap<BlogCreateVM, Blog>()
                .ForMember(dest => dest.MainImage, opt => opt.Ignore())
                .ForMember(dest => dest.BlogCategories, opt => opt.Ignore())
                .ForMember(dest => dest.BlogTags, opt => opt.Ignore());

            CreateMap<BlogUpdateVM, Blog>()
                .ForMember(dest => dest.MainImage, opt => opt.Ignore())
                .ForMember(dest => dest.BlogCategories, opt => opt.Ignore())
                .ForMember(dest => dest.BlogTags, opt => opt.Ignore());

            CreateMap<Blog, BlogDetailVM>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.BlogCategories))
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.BlogTags))
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments));
        }
    }
}
