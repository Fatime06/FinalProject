namespace Domain.Entities
{
    public class Review : Audit
    {
        public int Rating { get; set; }
        public string Text { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
