using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Migrations;
using System.Globalization;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            var userId = "A23A7A9F-11DD-48F0-B999-3D5AA6E5AB0E";
            var adminUserId = "7A9FJSKE-11DD-EB38-JS88-3D5AA6E5AB0E";
            var memberRoleId = "9A17F51D-AED3-4C8C-BE55-EE3D6E8A0C01";
            var adminRoleId = "F8A43D91-1E74-4F8A-BC55-5D27A3F9989A";
            var superAdminRoleId = "25D6D5B2-DC97-4042-B56E-EB3F8123BB99";
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN".ToUpper()
                },
                new IdentityRole
                {
                    Id = memberRoleId,
                    Name = "Member",
                    NormalizedName = "MEMBER".ToUpper()
                },
                new IdentityRole
                {
                    Id = superAdminRoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN".ToUpper()
                }
                );
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = userId,
                    Name = "Fatima",
                    Surname = "Asadova",
                    Address = "Bakı",
                    Gender = Domain.Enums.Gender.Woman,
                    UserName = "_fatima",
                    Birthday = DateTime.ParseExact("14-02-2006", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    NormalizedUserName = "_FATIMA",
                    EmailConfirmed = true,
                    Email = "esedovaf4@gmail.com",
                    NormalizedEmail = "ESEDOVAF4@GMAIL.COM",
                    PasswordHash = hasher.HashPassword(null, "Fatima123!")
                }
                );
            builder.Entity<AppUser>().HasData(
             new AppUser
             {
                 Id = adminUserId,
                 Name = "Fatya",
                 Surname = "Esedova",
                 Address = "Bakı",
                 Gender = Domain.Enums.Gender.Woman,
                 UserName = "_fatya",
                 Birthday = DateTime.ParseExact("14-02-2006", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                 NormalizedUserName = "_FATYA",
                 EmailConfirmed = true,
                 Email = "esedovaf6@gmail.com",
                 NormalizedEmail = "ESEDOVAF6@GMAIL.COM",
                 PasswordHash = hasher.HashPassword(null, "Fatima123!")
             }
             );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = userId,
                    RoleId = superAdminRoleId
                }
                );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = adminRoleId
                }
                );
            base.OnModelCreating(builder);
        }
    }
}
