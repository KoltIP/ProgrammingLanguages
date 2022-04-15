using ProgrammingLanguage.Web.Pages.Categories.Models;

namespace ProgrammingLanguage.Web.Pages.Categories.Services
{
    public interface ICategoryService
    {
        Task AddCategory(CategoryModel model);
        Task<CategoryListItem> GetCategory(int categoryId);
        Task<IEnumerable<CategoryListItem>> GetCategories(int offset = 0, int limit = 10);
        Task EditCategory(int categoryId, CategoryModel model);
        Task DeleteCategory(int categoryId);
    }
}
