using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Service;
using Service.Service.Interfaces;
using Service.Validators.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterServiceForService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            });
            services.AddValidatorsFromAssemblyContaining<CategoryCreateVMValidator>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRatingService, ProductRatingService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IContactMessageService, ContactMessageService>();
            services.AddScoped<IBasketService, BasketService>();
            return services;
        }
    }
}
