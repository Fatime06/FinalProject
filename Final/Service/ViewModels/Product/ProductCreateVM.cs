using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public int Quantity { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
