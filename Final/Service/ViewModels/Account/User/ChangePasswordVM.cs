using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Account.User
{
    public class ChangePasswordVM
    {
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
