using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly IServiceService _serviceService;

        public FooterViewComponent(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _serviceService.GetAllAsync();
            return View(services);
        }
    }
}
