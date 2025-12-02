namespace Domain.Entities
{
    public class History : Audit
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
