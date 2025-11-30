using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Slider;

namespace Service.AutoMappers
{
    public class SliderAutoMapper : Profile
    {
        public SliderAutoMapper()
        {
            CreateMap<SliderCreateVM, Slider>()
                  .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<SliderUpdateVM, Slider>().ReverseMap()
                  .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<Slider, SliderVM>();
        }
    }
}
