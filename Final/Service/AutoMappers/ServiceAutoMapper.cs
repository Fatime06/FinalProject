using AutoMapper;
using Service.ViewModels.Service;

namespace Service.AutoMappers
{
    public class ServiceAutoMapper : Profile
    {
        public ServiceAutoMapper()
        {
            CreateMap<ServiceCreateVM, Domain.Entities.Service>();
            CreateMap<ServiceUpdateVM, Domain.Entities.Service>().ReverseMap();
            CreateMap<Domain.Entities.Service, ServiceVM>();
        }
    }
}
