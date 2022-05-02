using ProgrammingLanguage.Web.Pages.Languages.Models;
using ProgrammingLanguages.Shared.Common.Responses;

namespace ProgrammingLanguage.Web.Pages.Languages.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentListItem>> GetComments(int languageId);
        Task<CommentListItem> GetComment(int id);
        Task<ErrorResponse> AddComment(CommentModel model);
        Task<ErrorResponse> UpdateComment(int commentId, CommentModel model);
        Task<ErrorResponse> DeleteComment(int commentId);

    }
}
