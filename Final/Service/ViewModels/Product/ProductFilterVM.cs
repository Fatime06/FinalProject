namespace Service.ViewModels.Product
{
    public class ProductFilterVM
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? Search { get; set; }
        public string? Sort { get; set; } 

        public int Page { get; set; } = 1;
    }
}
