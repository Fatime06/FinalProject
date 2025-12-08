using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Account.User;

namespace Final.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Register()
        {
            ViewBag.Genders = Enum.GetValues(typeof(Gender));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM vm)
        {
            var result = await _accountService.UserRegisterAsync(vm,ModelState);
            if (!result)
            {
                ViewBag.Genders = Enum.GetValues(typeof(Gender));
                return View(vm);
            }
            TempData["SuccessMessage"] = "Registration completed successfully! A confirmation link has been sent to your email address.";
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            await _accountService.EmailConfirm(email, token);
            TempData["SuccessMessage"] = "Your account has been successfully verified!";
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM vm, string? returnUrl)
        {
            if (!await _accountService.UserLoginAsync(vm, ModelState))
                return View(vm);
            return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("Index", "Home");
        }
    }
}
