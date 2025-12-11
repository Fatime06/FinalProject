using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class HistoryViewComponent : ViewComponent
    {
        private readonly IHistoryService _historyService;

        public HistoryViewComponent(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var history = await _historyService.GetAllAsync();
            return View(history);
        }
    }
}
