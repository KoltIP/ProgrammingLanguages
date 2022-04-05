using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageModel> AddLanguage(AddLanguageModel model);
        Task<LanguageModel> GetLanguage(int id);
        Task<IEnumerable<LanguageModel>> GetLanguages(int offset = 0, int limit = 10);       
        Task UpdateLanguage(int id, UpdateLanguageModel model);
        Task DeleteLanguage(int bookId);
    }
}
