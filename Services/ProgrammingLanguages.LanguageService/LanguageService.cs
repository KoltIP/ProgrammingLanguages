using ProgrammingLanguages.LanguageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.LanguageService
{
    public class LanguageService : ILanguageService
    {
        public async Task<LanguageModel> AddLanguage(AddLanguageModel model)
        {
            
        }

        public async Task<LanguageModel> GetLanguage(int id)
        {
            
        }

        public async Task<IEnumerable<LanguageModel>> GetLanguages()
        {
           
        }

        public async Task UpdateLanguage(int id, UpdateLanguageModel model)
        {
           
        }

        public async Task DeleteBook(int bookId)
        {
           
        }
    }
}
