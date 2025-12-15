using Service.ViewModels.BasketItem;

namespace Service.ViewModels.Order
{
    public class CheckoutVM
    {
        public List<BasketItemVM> Items { get; set; } = new();

        public decimal TotalPrice { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
