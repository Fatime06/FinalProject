namespace Domain.Entities
{
    public class Slider : Audit
    {
        public string Image { get; set; }
        public string SmallText { get; set; }
        public string BigText { get; set; }
        public string MediumText { get; set; }
        public string ButtonText { get; set; }
        public string? SmallNote { get; set; }
    }
}
