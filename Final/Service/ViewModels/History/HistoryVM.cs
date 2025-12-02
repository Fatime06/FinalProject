namespace Service.ViewModels.History
{
    public class HistoryVM
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
