using AutoMapper;
using Domain.Entities;
using Service.ViewModels.ContactMessage;

namespace Service.AutoMappers
{
    public class ContactMessageAutoMapper : Profile
    {
        public ContactMessageAutoMapper()
        {
            CreateMap<ContactMessageCreateVM, ContactMessage>();

            CreateMap<ContactMessage, ContactMessageVM>();
        }
    }
}
