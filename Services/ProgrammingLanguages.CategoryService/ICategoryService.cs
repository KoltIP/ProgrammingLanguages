using ProgrammingLanguages.CategoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CategoryService
{
    public interface ICategoryService
    {
        Task<CategoryModel> GetCategory(int id);
        Task<IEnumerable<CategoryModel>> GetCategories(int offset = 0, int limit = 10);
        Task<CategoryModel> AddCategory(AddCategoryModel model);
        Task UpdateCategory(int id, UpdateCategoryModel model);
        Task DeleteCategory(int categoryId);
    }
}
