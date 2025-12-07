using Microsoft.AspNetCore.Mvc;

namespace Final.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
