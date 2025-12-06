namespace Domain.Entities
{
    public class Tag : Audit
    {
        public string Name { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
