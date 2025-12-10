using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsVerifiedPurchase { get; set; } = false;
        public string? Image { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string? CustomerNumber { get; set; }
        public IEnumerable<ProductRating> ProductRatings { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public Basket Basket { get; set; }
    }
}
