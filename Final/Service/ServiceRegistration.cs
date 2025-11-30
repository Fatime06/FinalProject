using FluentValidation;
using FluentValidation.AspNetCore;
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
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CategoryCreateVMValidator>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRatingService, ProductRatingService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISliderService, SliderService>();
            return services;
        }
    }
}
