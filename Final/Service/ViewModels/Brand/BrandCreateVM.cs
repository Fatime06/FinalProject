using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Brand
{
    public class BrandCreateVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
