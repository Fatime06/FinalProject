using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.ViewModels.Comment;

namespace Service.Service.Interfaces
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CommentCreateVM vm, ModelStateDictionary modelState);
        Task<IEnumerable<CommentVM>> GetCommentsAsync(int blogId);
        Task DeleteCommentAsync(int commentId);
        Task<bool> EditCommentAsync(CommentEditVM vm, ModelStateDictionary modelState);
    }
}
