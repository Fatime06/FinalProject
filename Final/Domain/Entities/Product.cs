namespace Domain.Entities
{
    public class Product : Audit
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double? DiscountPrice { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ProductRating> Ratings { get; set; }
    }
}
