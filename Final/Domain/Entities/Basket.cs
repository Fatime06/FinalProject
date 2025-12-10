namespace Domain.Entities
{
    public class Basket : Audit
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
