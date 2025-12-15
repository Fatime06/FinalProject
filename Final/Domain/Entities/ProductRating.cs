namespace Domain.Entities
{
    public class ProductRating : Audit
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Value { get; set; }          
        public string? Comment { get; set; }
    }
}
