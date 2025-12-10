namespace Domain.Entities
{
    public class ContactMessage : Audit
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
