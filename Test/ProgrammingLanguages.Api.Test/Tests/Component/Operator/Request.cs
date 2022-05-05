using ProgrammingLanguages.Api.Controllers.Operators.Models;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
    {
        public static AddOperatorRequest AddOperatorRequest(int languageId, string name, string description)
        {
            return new AddOperatorRequest()
            {
                LanguageId = languageId,
                Name = name,
                Description = description
            };
        }

        public static UpdateOperatorRequest UpdateOperatorRequest(int languageId, string name, string description)
        {
            return new UpdateOperatorRequest()
            {
                languageId = languageId,
                Name = name,
                Description = description
            };
        }
    }
}
