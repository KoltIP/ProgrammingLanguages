namespace ProgrammingLanguage.Web.Pages.Languages.Models
{
    public interface ILanguageService
    {
        Task AddLanguage(LanguageModel model);
        Task<LanguageListItem> GetLanguage(int languageId);
        Task<IEnumerable<LanguageListItem>> GetLanguages(int offset = 0, int limit = 10);
        Task EditLanguage(int languageId, LanguageModel model);
        Task DeleteLanguage(int languageId);
        Task<IEnumerable<CategoryModel>> GetCategoryList();
        Task AddSubscribe(int languageId);
    }
}
