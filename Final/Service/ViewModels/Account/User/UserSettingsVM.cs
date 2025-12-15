using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Account.User
{
    public class UserSettingsVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

        public string Address { get; set; }

        public IFormFile? Image { get; set; }
        public string? CurrentImage { get; set; }
    }
}
