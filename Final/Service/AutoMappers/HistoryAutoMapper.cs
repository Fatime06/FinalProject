using AutoMapper;
using Domain.Entities;
using Service.ViewModels.History;

namespace Service.AutoMappers
{
    public class HistoryAutoMapper : Profile
    {
        public HistoryAutoMapper()
        {
            CreateMap<History, HistoryVM>();
            CreateMap<HistoryCreateVM, History>();
            CreateMap<HistoryUpdateVM, History>().ReverseMap();
        }
    }
}
