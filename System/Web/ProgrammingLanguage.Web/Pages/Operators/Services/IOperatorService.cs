using ProgrammingLanguage.Web.Pages.Operators.Models;
using ProgrammingLanguages.Shared.Common.Responses;

namespace ProgrammingLanguage.Web.Pages.Operators.Services
{
    public interface IOperatorService
    {
        Task<ErrorResponse> AddOperator(OperatorModel model);
        Task<OperatorListItem> GetOperator(int OperatorId);
        Task<IEnumerable<OperatorListItem>> GetOperators(int offset = 0, int limit = 10);
        Task<ErrorResponse> EditOperator(int operatorId, OperatorModel model);
        Task<ErrorResponse> DeleteOperator(int operatorId);
        Task<IEnumerable<LanguageModel>> GetLanguageList();
    }
}
