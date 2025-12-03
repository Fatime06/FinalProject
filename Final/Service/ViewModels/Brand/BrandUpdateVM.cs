using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Brand
{
    public class BrandUpdateVM
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}
