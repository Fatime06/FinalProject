using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Tag;

namespace Service.AutoMappers
{
    public class TagAutoMapper : Profile
    {
        public TagAutoMapper()
        {
            CreateMap<Tag, TagVM>();
            CreateMap<TagCreateVM, Tag>();
            CreateMap<TagUpdateVM, Tag>().ReverseMap();
        }
    }
}
