using System.ComponentModel.DataAnnotations;
namespace Service.ViewModels.Account.User
{
    public class UserLoginVM
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
