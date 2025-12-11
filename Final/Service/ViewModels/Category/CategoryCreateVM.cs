using Microsoft.AspNetCore.Http;

namespace Service.DTOs.Category
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
