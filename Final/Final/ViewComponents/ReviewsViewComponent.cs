using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class ReviewsViewComponent : ViewComponent
    {
        private readonly IReviewService _reviewService;

        public ReviewsViewComponent(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }
    }
}
