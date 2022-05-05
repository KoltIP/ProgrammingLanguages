using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common;
using ProgrammingLanguages.Shared.Common.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    [TestFixture]
    public partial class CategoryIntegrationTest : ComponentTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await using var context = await DbContext();

            context.Operators.RemoveRange(context.Operators);
            context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            //context.Comments.RemoveRange(context.Comments);
            context.SaveChanges();

            var category1 = new Db.Entities.Category()
            {
                Name = "OOP",
                Description = "first"
            };
            context.Categories.Add(category1);
            var category2 = new Db.Entities.Category()
            {
                Name = "neOOP",
                Description = "Second"
            };
            context.Categories.Add(category2);
                       

            context.SaveChanges();
        }

        [TearDown]
        public async override Task TearDown()
        {
            await using var context = await DbContext();
            //context.Operators.RemoveRange(context.Operators);
            //context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            //context.Comments.RemoveRange(context.Comments);
            context.SaveChanges();
            await base.TearDown();
        }

        protected static class Urls
        {
            public static string GetCategories(int? offset = null, int? limit = null)
            {

                if (offset is null && limit is null)
                    return $"/api/v1/category";
                List<string> queryParameters = new List<string>();

                if (offset.HasValue)
                {
                    queryParameters.Add($"offset={offset}");
                }

                if (limit.HasValue)
                {
                    queryParameters.Add($"limit={limit}");
                }

                var queryString = string.Join("&", queryParameters);
                return $"/api/v1/category?{queryString}";
            }

            public static string GetCategory(string id) => $"/api/v1/category/{id}";

            public static string DeleteCategory(string id) => $"/api/v1/category/{id}";

            public static string UpdateCategory(string id) => $"/api/v1/category/{id}";

            public static string AddCategory() => $"/api/v1/category";
        }

        public static class Scopes
        {
            public static string ReadLanguage => $"offline_access {AppScopes.LanguageRead}";

            public static string WriteLanguage => $"offline_access {AppScopes.LanguageWrite}";

            public static string ReadAndWriteLanguage => $"offline_access {AppScopes.LanguageRead} {AppScopes.LanguageWrite}";

            public static string Empty => "offline_access";
        }

        public async Task<string> AuthenticateUser_ReadAndWriteLanguageScope()
        {
            await GetTestUser();
            var tokenResponse = await AuthenticateTestUser("test@test.ru", "test", Scopes.ReadAndWriteLanguage);
            return tokenResponse.AccessToken;
        }

        public async Task<string> AuthenticateUser_EmptyScope()
        {
            await GetTestUser();
            var tokenResponse = await AuthenticateTestUser("test@test.ru", "test", Scopes.Empty);
            return tokenResponse.AccessToken;
        }

        public async Task<int> GetExistedCategoryId()
        {
            await using var context = await DbContext();
            if (context.Categories.Count() == 0)
            {
                Db.Entities.Category category = new Db.Entities.Category()
                {
                    Name = "Test",
                    Description = "testDescription"
                };
                context.Categories.Add(category);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var new_category = context1.Categories.AsEnumerable().First();
            return new_category.Id;
        }

        
        public async Task<int> GetNotExistedCategoryId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Categories.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }        
    }
}
