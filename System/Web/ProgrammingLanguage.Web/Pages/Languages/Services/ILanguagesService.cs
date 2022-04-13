namespace ProgrammingLanguage.Web.Pages.Languages.Models
{
    public interface ILanguageService
    {
        Task AddLanguage(LanguageModel model);
        Task<LanguageListItem> GetLanguage(int id);
        Task<IEnumerable<LanguageListItem>> GetLanguages(int offset = 0, int limit = 10);
        Task EditLanguage(int id, LanguageModel model);
        Task DeleteLanguage(int bookId);
        Task<IEnumerable<CategoryModel>> GetCategoryList();
    }
}
