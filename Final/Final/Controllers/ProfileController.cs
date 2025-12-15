using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Account.User;

namespace Final.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileService _fileService;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager, IFileService fileService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _fileService = fileService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var vm = new UserProfileVM
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Image = user.Image,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Address = user.Address,
                CustomerNumber = user.CustomerNumber
            };

            return View(vm);
        }
        public async Task<IActionResult> Settings()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(new UserSettingsVM
            {
                Name = user.Name,
                Surname = user.Surname,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Address = user.Address,
                CurrentImage = user.Image
            });
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UserSettingsVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await _userManager.GetUserAsync(User);

            user.Name = vm.Name;
            user.Surname = vm.Surname;
            user.Birthday = vm.Birthday;
            user.Gender = vm.Gender;
            user.Address = vm.Address;

            var image = user.Image;


            if (vm.Image != null)
            {
                _fileService.Delete(user.Image, "client/uploads/users");

                user.Image = await _fileService.UploadAsync(vm.Image, "client/uploads/users");
            }
            else
            {
                user.Image = image;
            }

            await _userManager.UpdateAsync(user);

            TempData["Success"] = "Profile updated successfully";

            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var result = await _userManager.ChangePasswordAsync(
                user,
                vm.CurrentPassword,
                vm.NewPassword
            );

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                return View(vm);
            }

            await _signInManager.RefreshSignInAsync(user);

            TempData["Success"] = "Password changed successfully";
            return RedirectToAction("Index");
        }

    }
}
