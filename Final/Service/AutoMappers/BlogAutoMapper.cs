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
                     opt => opt.MapFrom(src => src.Comments.Count()))
                .ForMember(dest => dest.Image,
                     opt => opt.MapFrom(src => src.MainImage));

            CreateMap<BlogCreateVM, Blog>()
                .ForMember(dest => dest.MainImage, opt => opt.Ignore())
                .ForMember(dest => dest.BlogCategories, opt => opt.MapFrom(src =>
                     src.CategoryIds.Select(id => new BlogCategory { CategoryId = id }).ToList()))
                .ForMember(dest => dest.BlogTags, opt => opt.MapFrom(src =>
                     src.TagIds.Select(id => new BlogTag { TagId = id }).ToList()));

            CreateMap<BlogUpdateVM, Blog>()
                .ForMember(dest => dest.MainImage, opt => opt.Ignore())
                .ForMember(dest => dest.BlogCategories, opt => opt.MapFrom(src =>
                     src.CategoryIds.Select(id => new BlogCategory { CategoryId = id }).ToList()))
                .ForMember(dest => dest.BlogTags, opt => opt.MapFrom(src =>
                     src.TagIds.Select(id => new BlogTag { TagId = id }).ToList()));
            CreateMap<Blog,BlogUpdateVM>()
                .ForMember(dest => dest.CategoryIds,
                    opt => opt.MapFrom(src => src.BlogCategories.Select(bc => bc.CategoryId).ToList()))
                .ForMember(dest => dest.TagIds,
                    opt => opt.MapFrom(src => src.BlogTags.Select(bt => bt.TagId).ToList()));


            CreateMap<Blog, BlogDetailVM>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.BlogCategories))
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.BlogTags))
                .ForMember(dest => dest.Comments,
                    opt => opt.MapFrom(src => src.Comments));
            CreateMap<BlogCategory, CategoryInBlogVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BlogTag, TagInBlogVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Tag.Name));
        }
    }
}
