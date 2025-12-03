namespace Service.ViewModels.Review
{
    public class ReviewVM
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public UserInReview AppUser { get; set; }
    }
}
