using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Helpers;
using Service.Service.Interfaces;
using Service.ViewModels.Account.Admin;
using Service.ViewModels.Account.User;
using Service.ViewModels.Email;
using System.Security.Claims;

namespace Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileService;
        private readonly IBasketService _basketService;

        public AccountService(IAccountRepository accountRepo, IMapper mapper, IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor, IHttpContextAccessor httpContextAccessor, IEmailService emailService, IFileService fileService, IBasketService basketService)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext ?? new());
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _fileService = fileService;
            _basketService = basketService;
        }

        public async Task LogoutAsync()
        {
            await _accountRepo.SignOutAsync();
        }
        public async Task EmailConfirm(string email, string token)
        {
            var user = await _accountRepo.FindByEmailAsync(email);
            if (user == null || !await _accountRepo.IsInRoleAsync(user, "Member")) throw new CustomException(404, "An unexpected error occurred.");
            if (user.EmailConfirmed)
                throw new CustomException(400, "This email has already been verified.");
            await _accountRepo.EmailConfirmAsync(user, token);
        }

        public async Task<bool> UserRegisterAsync(UserRegisterVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;
            AppUser user = await _accountRepo.FindByEmailAsync(vm.Email);

            if (user != null)
            {
                modelState.AddModelError("Email", "This email already exists");
                return false;
            }
            var existUser = await _accountRepo.GetUserAsync(vm.UserName);
            if (existUser != null)
            {
                modelState.AddModelError("UserName", "This username already exists");
                return false;
            }
            user = _mapper.Map<AppUser>(vm);
            user.CustomerNumber = UserHelper.GenerateCustomerNumber();
            user.Image = await _fileService.UploadAsync(vm.Image, "client/uploads/users");
            var result = await _accountRepo.RegisterAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError("", error.Description);
                }
                return false;
            }
            await _accountRepo.AddRoleToUserAsync(user, "Member");

            var token = await _accountRepo.GenerateEmailConfirmationTokenAsync(user);
            var url = _urlHelper.Action("VerifyEmail", "Account", new { email = user.Email, token = token }, _httpContextAccessor.HttpContext.Request.Scheme);
            using StreamReader reader = new StreamReader("wwwroot/templates/emailConfirm.html");
            var body = reader.ReadToEnd();
            body = body.Replace("{{url}}", url);
            body = body.Replace("{{name}}", user.Name);
            body = body.Replace("{{surname}}", user.Surname);
            EmailSendVM emailVm = new()
            {
                Subject = "Confirm Email",
                Body = body,
                To = user.Email
            };
            _emailService.SendEmailAsync(emailVm);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(PasswordResetVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var user = await _accountRepo.FindByEmailAsync(vm.Email);
            if (user == null || !await _accountRepo.IsInRoleAsync(user, "Member")) throw new CustomException(404, "User not found");
            if (!await _accountRepo.VerifyUserTokenAsync(user, "ResetPassword", vm.Token))
                throw new CustomException(404, "An unexpected error occurred.");
            var result = await _accountRepo.ResetPasswordAsync(user, vm.Token, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError("", error.Description);
                }
                return false;
            }
            return true;
        }

        public async Task<bool> UserLoginAsync(UserLoginVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var user = await _accountRepo.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                modelState.AddModelError("", "Incorrect email or password!");
                return false;
            }

            if (!await _accountRepo.IsInRoleAsync(user, "Member"))
            {
                modelState.AddModelError("", "This user cannot log in!");
                return false;
            }
            if (!user.EmailConfirmed)
            {
                modelState.AddModelError("", "You have not verified your email. Please check the confirmation email and activate your account.");
                return false;
            }
            var result = await _accountRepo.PasswordSignInAsync(user, vm.Password, false, true);
            if (result.IsLockedOut)
            {
                modelState.AddModelError("", "Account blocked.");
                return false;
            }
            if (!result.Succeeded)
            {
                modelState.AddModelError("", "Incorrect email/username or password!");
                return false;
            }
            await _basketService.MergeCookieBasketToDbAsync(user.Id);
            return true;
        }
        public async Task<AppUser> GetUserAsync(string username)
        {
            return await _accountRepo.GetUserAsync(username);
        }
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var user = await _accountRepo.FindByEmailAsync(vm.Email);
            if (user == null || !await _accountRepo.IsInRoleAsync(user, "Member"))
            {
                modelState.AddModelError("Email", "Email not found");
                return false;
            }
            var token = await _accountRepo.GeneratePasswordResetTokenAsync(user);
            var url = _urlHelper.Action("VerifyPassword", "Account", new { email = user.Email, token = token }, _httpContextAccessor.HttpContext.Request.Scheme);


            using StreamReader reader = new StreamReader("wwwroot/templates/resetpassword.html");
            var body = reader.ReadToEnd();
            body = body.Replace("{{url}}", url);
            body = body.Replace("{{name}}", user.Name);
            body = body.Replace("{{surname}}", user.Surname);

            EmailSendVM emailSendVm = new()
            {
                Body = body,
                Subject = "Reset Password",
                To = vm.Email
            };

            _emailService.SendEmailAsync(emailSendVm);
            return true;
        }
        public async Task VerifyPasswordAsync(string token, string email)
        {
            var user = await _accountRepo.FindByEmailAsync(email);
            if (user == null || !await _accountRepo.IsInRoleAsync(user, "Member")) throw new CustomException(404, "User not found");
            if (!await _accountRepo.VerifyUserTokenAsync(user, "ResetPassword", token))
                throw new CustomException(404, "An unexpected error occurred!");
        }

        public async Task<bool> AdminLoginAsync(AdminLoginVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return false;

            var user = await _accountRepo.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                modelState.AddModelError("", "Email or password is incorrect!");
                return false;
            }

            // ✅ Rol adlarını seed-dəki kimi yaz
            var isAdmin = await _accountRepo.IsInRoleAsync(user, "Admin");
            var isSuperAdmin = await _accountRepo.IsInRoleAsync(user, "SuperAdmin");

            if (!isAdmin && !isSuperAdmin)
            {
                modelState.AddModelError("", "You do not have permission to access this section!");
                return false;
            }

            if (!await _accountRepo.CheckPasswordAsync(user, vm.Password))
            {
                modelState.AddModelError("", "Email or password is incorrect!");
                return false;
            }

            // ✅ Köhnə cookie/claim qalmasın
            await _httpContextAccessor.HttpContext!.SignOutAsync("AdminScheme");

            // ✅ DB-dən real rolları götür və claim kimi əlavə et
            var roles = await _accountRepo.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""),
        new Claim(ClaimTypes.Email, user.Email ?? "")
    };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var identity = new ClaimsIdentity(claims, "AdminScheme");
            var principal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext.SignInAsync("AdminScheme", principal);

            return true;
        }
    }
}
