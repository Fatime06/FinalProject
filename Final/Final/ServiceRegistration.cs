using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Final
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServiceForFinal(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultDatabase"));
            });
            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddIdentity<AppUser, IdentityRole>()
     .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Events.OnRedirectToLogin = opt.Events.OnRedirectToAccessDenied = context =>
                {
                    var uri = new Uri(context.RedirectUri);
                    if (context.Request.Path.Value.ToLower().StartsWith("/admin"))
                    {

                        context.Response.Redirect("/admin/account/login" + uri.Query);
                    }
                    else
                    {
                        context.Response.Redirect("/account/login" + uri.Query);
                    }

                    return Task.CompletedTask;
                };
            });
            return services;
        }
    }
}
