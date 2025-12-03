using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsVerifiedPurchase { get; set; } = false;
        public string Image { get; set; }
        public IEnumerable<ProductRating> ProductRatings { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
