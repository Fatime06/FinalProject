using Service.ViewModels.BasketItem;

namespace Service.ViewModels.Basket
{
    public class BasketUIVM
    {
        public List<BasketItemVM> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalCount { get; set; }
    }
}
