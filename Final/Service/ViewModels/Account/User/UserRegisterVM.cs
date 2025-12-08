using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Account.User
{
    public class UserRegisterVM
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }
}
