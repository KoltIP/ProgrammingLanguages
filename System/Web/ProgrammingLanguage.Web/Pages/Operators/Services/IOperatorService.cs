using ProgrammingLanguage.Web.Pages.Operators.Models;

namespace ProgrammingLanguage.Web.Pages.Operators.Services
{
    public interface IOperatorService
    {
        Task AddOperator(OperatorModel model);
        Task<OperatorListItem> GetOperator(int OperatorId);
        Task<IEnumerable<OperatorListItem>> GetOperators(int offset = 0, int limit = 10);
        Task EditOperator(int operatorId, OperatorModel model);
        Task DeleteOperator(int operatorId);
        Task<IEnumerable<LanguageModel>> GetLanguageList();
    }
}
