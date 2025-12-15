using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        [Authorize(AuthenticationSchemes = "AdminScheme")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
