namespace Service.ViewModels.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public double Price { get; set; }
        public double? DiscountPrice { get; set; }

        public int Quantity { get; set; }
        public bool InStock { get; set; }

        public double AverageRating { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }

}
