using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Account.User;

namespace Service.Service.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UserRegisterAsync(UserRegisterVM model,ModelStateDictionary modelState);
        Task<bool> UserLoginAsync(UserLoginVM vm, ModelStateDictionary modelState);
        Task LogoutAsync();
        Task EmailConfirm(string email, string token);
        Task<bool> ResetPasswordAsync(PasswordResetVM vm, ModelStateDictionary modelState);
        Task<AppUser> GetUserAsync(string userName);
        Task<bool> ForgotPasswordAsync(ForgotPasswordVM vm, ModelStateDictionary modelState);
        Task VerifyPasswordAsync(string token, string email);
    }
}
