using Service.ViewModels.ProductRating;

namespace Service.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public CategoryInProductVM Category { get; set; }
        public BrandInProduct Brand { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<ProductRatingVM> Ratings { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
