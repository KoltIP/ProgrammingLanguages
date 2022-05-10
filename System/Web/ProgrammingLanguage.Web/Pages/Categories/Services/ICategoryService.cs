using ProgrammingLanguage.Web.Pages.Categories.Models;
using ProgrammingLanguages.Shared.Common.Responses;

namespace ProgrammingLanguage.Web.Pages.Categories.Services
{
    public interface ICategoryService
    {
        Task<ErrorResponse> AddCategory(CategoryModel model);
        Task<CategoryListItem> GetCategory(int categoryId);
        Task<IEnumerable<CategoryListItem>> GetCategories(int offset = 0, int limit = 10);
        Task<ErrorResponse> EditCategory(int categoryId, CategoryModel model);
        Task<ErrorResponse> DeleteCategory(int categoryId);
    }
}
