using Service.ViewModels.BasketItem;

namespace Service.ViewModels.Basket
{
    public class BasketVM
    {
        public List<BasketItemVM> Items { get; set; }
        public double TotalPrice { get; set; }
    }
}
