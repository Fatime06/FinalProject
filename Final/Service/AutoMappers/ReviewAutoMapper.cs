using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Review;

namespace Service.AutoMappers
{
    public class ReviewAutoMapper : Profile
    {
        public ReviewAutoMapper()
        {
            CreateMap<ReviewCreateVM, Review>()
                .ForMember(dest => dest.AppUserId, opt => opt.Ignore());
            CreateMap<ReviewUpdateVM, Review>().ReverseMap();
            CreateMap<Review, ReviewVM>()
                .ForMember(src => src.AppUser, opt => opt.MapFrom(dest => dest.AppUser));
        }
    }
}
