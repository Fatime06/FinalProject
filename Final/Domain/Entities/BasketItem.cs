namespace Domain.Entities
{
    public class BasketItem : Audit
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
