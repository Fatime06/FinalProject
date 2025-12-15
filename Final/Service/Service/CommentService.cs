using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Exceptions;
using Service.Service.Interfaces;
using Service.ViewModels.Comment;
using System.Security.Claims;

namespace Service.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepo, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<bool> AddCommentAsync(CommentCreateVM vm, ModelStateDictionary modelState)
        {
            var userId = _httpContextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comment = new Comment
            {
                BlogId = vm.BlogId,
                AppUserId = userId,
                Message = vm.Text,
                CreatedDate = DateTime.UtcNow
            };

            await _commentRepo.AddAsync(comment);
            await _commentRepo.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CommentVM>> GetCommentsAsync(int blogId)
        {
            var comments = await _commentRepo.GetAll()
                .Where(c => c.BlogId == blogId)
                .Include(c => c.AppUser)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<CommentVM>>(comments);

            return result;
        }
        public async Task DeleteCommentAsync(int commentId)
        {
            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comment = await _commentRepo.GetAll()
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                throw new CustomException(404, "Comment not found");

            if (comment.AppUserId != userId)
                throw new CustomException(403, "You cannot delete this comment");

            _commentRepo.Delete(comment);
            await _commentRepo.SaveChangesAsync();
        }
        public async Task<bool> EditCommentAsync(CommentEditVM vm,ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) return false;

            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comment = await _commentRepo.GetAll()
                .FirstOrDefaultAsync(c => c.Id == vm.CommentId);

            if (comment == null)
                throw new CustomException(404, "Comment not found");

            if (comment.AppUserId != userId)
                throw new CustomException(403, "You cannot edit this comment");

            comment.Message = vm.Text;
            await _commentRepo.SaveChangesAsync();
            return true;
        }
    }
}
