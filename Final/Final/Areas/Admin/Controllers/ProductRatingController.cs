using Microsoft.AspNetCore.Mvc;

namespace Final.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductRatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
