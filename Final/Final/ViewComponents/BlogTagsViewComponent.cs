using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace Final.ViewComponents
{
    public class BlogTagsViewComponent : ViewComponent
    {
        private readonly ITagService _tagService;

        public BlogTagsViewComponent(ITagService tagService)
        {
            _tagService = tagService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagService.GetAllAsync();
            return View(tags);
        }
    }
}
