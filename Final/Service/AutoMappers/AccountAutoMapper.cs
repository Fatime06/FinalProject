using AutoMapper;
using Domain.Entities;
using Service.ViewModels.Account.User;

namespace Service.AutoMappers
{
    public class AccountAutoMapper :  Profile
    {
        public AccountAutoMapper()
        {
            CreateMap<UserRegisterVM, AppUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
