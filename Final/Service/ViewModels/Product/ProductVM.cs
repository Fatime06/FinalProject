using Service.ViewModels.ProductRating;

namespace Service.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public CategoryInProductVM Category { get; set; }
        public double AverageRating { get; set; }
        public IEnumerable<ProductRatingVM> Ratings { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
