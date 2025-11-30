using Microsoft.AspNetCore.Http;

namespace Service.ViewModels.Slider
{
    public class SliderCreateVM
    {
        public IFormFile Image { get; set; }
        public string SmallText { get; set; }
        public string BigText { get; set; }
        public string MediumText { get; set; }
        public string ButtonText { get; set; }
        public string? SmallNote { get; set; }
    }
}
