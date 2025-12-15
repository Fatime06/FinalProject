using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Comment;

namespace Service.AutoMappers
{
    public class CommentAutoMapper : Profile
    {
        public CommentAutoMapper()
        {
            CreateMap<Comment, CommentVM>()
                 .ForMember(dest => dest.AppUser,
                     opt => opt.MapFrom(s => s.AppUser))
                 .ForMember(dest => dest.Text,
                     opt => opt.MapFrom(s => s.Message));
            
            CreateMap<AppUser, UserInComment>();
        }
    }
}
