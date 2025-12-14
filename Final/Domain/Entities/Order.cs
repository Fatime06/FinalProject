namespace Domain.Entities
{
    public class Order : Audit
    {
        public string AppUserId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
