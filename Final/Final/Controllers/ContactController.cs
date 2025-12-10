using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Service.Service.Interfaces;
    using Service.ViewModels.ContactMessage;

    public class ContactController : Controller
    {
        private readonly IContactMessageService _contactService;

        public ContactController(IContactMessageService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactMessageCreateVM vm)
        {
            bool result = await _contactService.CreateAsync(vm, ModelState);

            if (!result) return View(nameof(Index),vm);

            TempData["SuccessMessage"] = "Your message has been sent!";
            return RedirectToAction(nameof(Index));
        }
    }

}
