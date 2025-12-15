using Domain.Enums;

namespace Service.ViewModels.Order
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CustomerNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<OrderItemVM> Items { get; set; }
    }
}
