using Domain.Enums;

namespace Domain.Entities
{
    public class Order : Audit
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
