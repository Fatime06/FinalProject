using Service.ViewModels.Category;

namespace Service.DTOs.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<ProductInCategoryVM> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
