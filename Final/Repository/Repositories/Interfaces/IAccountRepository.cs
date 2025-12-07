using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<AppUser> GetAsync(string userId);
        Task<IdentityResult> RegisterAsync(AppUser user, string password);
        Task<AppUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
        Task AddRoleToUserAsync(AppUser user, string roleName);
        Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        Task<bool> IsInRoleAsync(AppUser user, string role);
        Task EmailConfirmAsync(AppUser user, string token);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<bool> VerifyUserTokenAsync(AppUser user, string purpose, string token);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string password);
        Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
