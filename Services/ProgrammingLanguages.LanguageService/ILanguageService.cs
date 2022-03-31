
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.LanguageService
{
    public interface ILanguageService
    {
        Task<LanguageModel> AddLanguage(AddLanguageModel model);
        Task<LanguageModel> GetLanguage(int id);
        Task<IEnumerable<LanguageModel>> GetLanguages();       
        Task UpdateLanguage(int id, UpdateLanguageModel model);
        Task DeleteBook(int bookId);
    }
}
