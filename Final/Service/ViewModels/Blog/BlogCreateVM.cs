using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Blog
{
    public class BlogCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public List<int> CategoryIds { get; set; } 
        public List<int> TagIds { get; set; }
    }
}
