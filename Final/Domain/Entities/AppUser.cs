using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public IEnumerable<ProductRating> ProductRatings { get; set; }
    }
}
