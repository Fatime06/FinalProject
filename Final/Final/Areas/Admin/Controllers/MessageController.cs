using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Data;
using Service.Service;
using Service.Service.Interfaces;
using Service.ViewModels.ContactMessage;
using Service.ViewModels.History;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "Admin,SuperAdmin"
)]
    public class MessageController : Controller
    {
        private readonly IContactMessageService _messageService;

        public MessageController(IContactMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 5;

            var query = _messageService.GetHistoriesQuery();

            var model = await PaginatedList<ContactMessageVM>.CreateAsync(
                query,
                page,
                pageSize
            );

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var message = await _messageService.GetAsync(id);
            return View(message);
        }
        [Authorize(
    AuthenticationSchemes = "AdminScheme",
    Roles = "SuperAdmin"
)]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Message successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
