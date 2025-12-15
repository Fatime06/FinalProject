using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using Service.ViewModels.Blog;
using Service.ViewModels.Comment;

namespace Final.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;

        public BlogController(IBlogService blogService, ICommentService commentService)
        {
            _blogService = blogService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page < 1) page = 1;

            int pageSize = 3;

            var query = _blogService.GetBlogsQuery();

            var vmQuery = query
                .OrderByDescending(b => b.CreatedDate)
                .Select(b => new BlogVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Image = b.MainImage,
                    CreatedAt = b.CreatedDate,
                    CommentCount = b.Comments.Count()
                });

            var model = await PaginatedList<BlogVM>.CreateAsync(
                vmQuery,
                page,
                pageSize
            );

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetAsync(id);
            return View(blog);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(CommentCreateVM vm)
        {
            var result = await _commentService.AddCommentAsync(vm, ModelState);
            if(!result)
            {
                var blog = await _blogService.GetAsync(vm.BlogId);
                return View("Detail", blog);
            }

            var comments = await _commentService.GetCommentsAsync(vm.BlogId);

            return PartialView("_BlogCommentsPartial", comments);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id, int blogId)
        {
            await _commentService.DeleteCommentAsync(id);

            var comments = await _commentService.GetCommentsAsync(blogId);
            return PartialView("_BlogCommentsPartial", comments);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] CommentEditVM vm)
        {
            if(!await _commentService.EditCommentAsync(vm,ModelState))
            {
                var blog = await _blogService.GetAsync(vm.BlogId);
                return View("Detail", blog);
            }

            var comments = await _commentService.GetCommentsAsync(vm.BlogId);
            return PartialView("_BlogCommentsPartial", comments);
        }
    }
}
