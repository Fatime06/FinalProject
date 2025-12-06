namespace Service.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
