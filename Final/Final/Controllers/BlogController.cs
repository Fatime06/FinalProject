using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
