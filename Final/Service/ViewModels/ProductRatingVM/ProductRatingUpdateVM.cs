namespace Service.ViewModels.ProductRatingVM
{
    public class ProductRatingUpdateVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Value { get; set; }
        public string? Comment { get; set; }
    }
}
