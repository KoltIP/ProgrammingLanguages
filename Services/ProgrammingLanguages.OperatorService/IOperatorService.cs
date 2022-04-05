using ProgrammingLanguages.LanguageService.Models;
using ProgrammingLanguages.OperatorService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.OperatorService
{
    public interface IOperatorService
    {
        Task<OperatorModel> AddOperator(AddOperatorModel model);
        Task<OperatorModel> GetOperator(int id);
        Task<IEnumerable<OperatorModel>> GetOperators(int offset = 0, int limit = 10);
        Task UpdateOperator(int id, UpdateOperatorModel model);
        Task DeleteOperator(int bookId);
    }
}
