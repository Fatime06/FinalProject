namespace Domain.Entities
{
    public class Category : Audit
    {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<BlogCategory> BlogCategories { get; set; }
    }
}
