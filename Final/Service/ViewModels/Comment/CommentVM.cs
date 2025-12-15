namespace Service.ViewModels.Comment
{
    public class CommentVM
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public UserInComment AppUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public int BlogId { get; set; }
    }
}
