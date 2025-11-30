using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
        public string SmallText { get; set; }
        public string BigText { get; set; }
        public string MediumText { get; set; }
        public string ButtonText { get; set; }
        public string? SmallNote { get; set; }
    }
}
