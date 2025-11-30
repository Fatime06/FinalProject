using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Service.ViewModels.ProductRatingVM;

namespace Service.AutoMappers
{
    public class ProductRatingMapper : Profile
    {
        public ProductRatingMapper()
        {
            CreateMap<ProductRatingCreateVM, ProductRating>();
            CreateMap<ProductRatingUpdateVM, ProductRating>();
            CreateMap<ProductRating, ProductRatingVM>();
            CreateMap<AppUser, AppUserInProductRatingVM>();
            CreateMap<Product, ProductInProductRatingVM>();
        }
    }
}
