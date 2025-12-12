namespace Domain.Entities
{
    public class Brand : Audit
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
