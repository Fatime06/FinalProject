using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.DTOs.CommonDtos;
using System.Diagnostics;

namespace Final.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error(string? json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                string decodedJson = Uri.UnescapeDataString(json);

                var dto = JsonConvert.DeserializeObject<ErrorDto>(decodedJson);
                dto.Timestamp = DateTime.Now;
                return View(dto);
            }

            return View(new ErrorDto
            {
                StatusCode = 500,
                Message = "An unexpected error occurred..",
                Timestamp = DateTime.Now
            });
        }
    }
}
