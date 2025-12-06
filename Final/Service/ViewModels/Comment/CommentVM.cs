namespace Service.ViewModels.Comment
{
    public class CommentVM
    {
        public string FullName { get; set; }
        public string Text { get; set; }
        public UserInComment AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
