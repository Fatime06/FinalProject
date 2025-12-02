using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.ProductRating;

namespace Service.Service
{
    public class ProductRatingService : IProductRatingService
    {
        private readonly IProductRatingRepository _ratingRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductRatingService(IProductRatingRepository ratingRepo, IProductRepository productRepo, IMapper mapper)
        {
            _ratingRepo = ratingRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ProductRatingCreateVM vm, ModelStateDictionary modelState)
        {
            var productExists = await _productRepo.Find(vm.ProductId).FirstOrDefaultAsync();
            if (productExists == null)
            {
                modelState.AddModelError("ProductId", "This product doesn`t exist");
                return false;
            }
            if (vm.Value < 1 || vm.Value > 5)
            {
                modelState.AddModelError("Rating", "Rating must be between 1 and 5.");
                return false;
            }
            var rating = _mapper.Map<ProductRating>(vm);
            rating.CreatedDate = DateTime.Now;
            await _ratingRepo.AddAsync(rating);
            await _ratingRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var rating = await _ratingRepo.Find(id).FirstOrDefaultAsync();
            if (rating != null)
            {
                _ratingRepo.Delete(rating);
                await _ratingRepo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductRatingVM>> GetAllAsync()
        {
            var ratings = await _ratingRepo.GetAll()
                .Include(r => r.Product)
                .Include(r => r.AppUser)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductRatingVM>>(ratings);
        }

        public async Task<ProductRatingVM> GetAsync(int id)
        {
            var rating = await _ratingRepo.GetAll()
                .Include(r => r.Product)
                .Include(r => r.AppUser)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rating == null) throw new CustomException(404, "Rating not found");

            return _mapper.Map<ProductRatingVM>(rating);
        }

        public async Task<ProductRatingUpdateVM> GetUpdatedVmAsync(int id)
        {
            var rating = await _ratingRepo.Find(id).Include(pr=>pr.Product).Include(pr=>pr.AppUser).FirstOrDefaultAsync();
            return _mapper.Map<ProductRatingUpdateVM>(rating);
        }

        public async Task<bool> UpdateAsync(ProductRatingUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var rating = await _ratingRepo.Find(vm.Id).Include(pr => pr.Product).Include(r => r.AppUser).FirstOrDefaultAsync();
            if (rating == null)
            {
                modelState.AddModelError("", "No such rating exists");
                return false;
            }

            if (vm.Value < 1 || vm.Value > 5)
            {
                modelState.AddModelError("Rating", "Rating must be between 1 and 5.");
                return false;
            }

            _mapper.Map(vm, rating);

            _ratingRepo.Update(rating);
            await _ratingRepo.SaveChangesAsync();

            return true;
        }
    }
}
