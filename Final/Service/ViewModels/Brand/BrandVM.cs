namespace Service.ViewModels.Brand
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<ProductInBrandVM> Products { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
