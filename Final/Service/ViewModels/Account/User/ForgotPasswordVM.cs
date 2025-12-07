using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Account.User
{
    public class ForgotPasswordVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
