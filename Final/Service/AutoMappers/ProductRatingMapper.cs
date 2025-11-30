using AutoMapper;
using Domain.Entities;
using Service.ViewModels.ProductRating;

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
