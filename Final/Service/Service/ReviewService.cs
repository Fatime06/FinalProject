using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Review;
using System.Security.Claims;

namespace Service.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;

        public ReviewService(IReviewRepository reviewRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<bool> CreateAsync(ReviewCreateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                modelState.AddModelError("", "User is not authenticated");
                return false;
            }
            var review = _mapper.Map<Review>(vm);
            review.AppUserId = userId;
            await _reviewRepo.AddAsync(review);
            await _reviewRepo.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewRepo.Find(id).FirstOrDefaultAsync();
            if (review == null) return;
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new CustomException(401, "User is not authenticated");
            if (review.AppUserId != userId) throw new CustomException(401, "You don't have permission");

            _reviewRepo.Delete(review);
            await _reviewRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewVM>> GetAllAsync()
        {
            var reviews = await _reviewRepo.GetAll().Include(r => r.AppUser)
                 .OrderByDescending(r => r.CreatedDate)
                 .ToListAsync();

            return _mapper.Map<IEnumerable<ReviewVM>>(reviews);
        }

        public async Task<ReviewVM> GetAsync(int id)
        {
            var review = await _reviewRepo.Find(id).Include(r=>r.AppUser).FirstOrDefaultAsync();
            if (review == null) throw new CustomException(404,"Review not found");

            return _mapper.Map<ReviewVM>(review);
        }

        public async Task<ReviewUpdateVM> GetUpdatedVmAsync(int id)
        {
            var review = await _reviewRepo.Find(id).SingleOrDefaultAsync();
            if (review == null) throw new CustomException(404, "Review not found");

            return _mapper.Map<ReviewUpdateVM>(review);
        }

        public async Task<bool> UpdateAsync(ReviewUpdateVM vm, ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var review = await _reviewRepo.Find(vm.Id).FirstOrDefaultAsync();

            if (review == null)
            {
                modelState.AddModelError("", "Review not found");
                return false;
            }

            _mapper.Map(vm, review);

            await _reviewRepo.SaveChangesAsync();
            return true;
        }
    }
}
