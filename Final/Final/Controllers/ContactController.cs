using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
