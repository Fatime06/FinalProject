using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
