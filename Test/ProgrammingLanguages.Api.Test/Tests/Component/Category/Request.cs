using ProgrammingLanguages.Api.Controllers.Categories.Models;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        public static AddCategoryRequest AddCategoryRequest(string name, string description)
        {
            return new AddCategoryRequest()
            {
                Name = name,
                Description = description
            };
        }

        public static UpdateCategoryRequest UpdateCategoryRequest(string name, string description)
        {
            return new UpdateCategoryRequest()
            {
                Name = name,
                Description = description
            };
        }
    }
}
