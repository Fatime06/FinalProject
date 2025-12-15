namespace Service.ViewModels.ProductRating
{
    public class ProductRatingVM
    {
        public int Id { get; set; }
        public ProductInProductRatingVM Product { get; set; }
        public AppUserInProductRatingVM AppUser { get; set; }
        public int OrderId { get; set; }
        public int Value { get; set; }
        public string? Comment { get; set; }
    }
}
