using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

public class HeaderViewComponent : ViewComponent
{
    private readonly IBasketService _basketService;

    public HeaderViewComponent(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = await _basketService.GetBasketAsync();
        return View(vm);
    }
}
