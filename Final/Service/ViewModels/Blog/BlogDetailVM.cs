using Service.ViewModels.Comment;

namespace Service.ViewModels.Blog
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserInBlogVM AppUser { get; set; }
        public IEnumerable<CategoryInBlogVM> Categories { get; set; }
        public IEnumerable<TagInBlogVM> Tags { get; set; }
        public IEnumerable<CommentVM> Comments { get; set; }
    }
}
