using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Test.Tests.Unit.Language
{
    public partial class LanguageUnitTest
    {
        public static UpdateLanguageModel UpdateLanguageModel(int categoryId, string name, string description)
        {
            return new UpdateLanguageModel()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description
            };
        }

        public static AddLanguageModel AddLanguageModel(int categoryId, string name, string description)
        {
            return new AddLanguageModel()
            {
                CategoryId = categoryId,
                Name = name,
                Description = description
            };
        }
    }
}
