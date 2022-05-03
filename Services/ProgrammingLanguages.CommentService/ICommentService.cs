using ProgrammingLanguages.CommentService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetCommentsByLanguageId(int idAuto);
        Task<CommentModel> GetCommentByCommentId(int id);
        Task<CommentModel> AddComment(AddCommentModel model);
        Task UpdateComment(int id, UpdateCommentModel model);
        Task DeleteComment(int id);
    }
}
