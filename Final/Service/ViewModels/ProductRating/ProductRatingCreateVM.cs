namespace Service.ViewModels.ProductRating
{
    public class ProductRatingCreateVM
    {
        public int ProductId { get; set; }
        public int Value { get; set; }
        public int OrderId { get; set; }
        public string? Comment { get; set; }
    }
}
