using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? DiscountPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
