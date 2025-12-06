namespace Domain.Entities
{
    public class Blog : Audit
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainImage { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<BlogCategory> BlogCategories { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
