using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common;
using ProgrammingLanguages.Shared.Common.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    [TestFixture]
    public partial class OperatorIntegrationTest : ComponentTest
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

            var language1 = new Db.Entities.Language()
            {
                Name = "Java",
                Description = "descriptionOne",
                Category = category1
            };
            context.Languages.Add(language1);

            var language2 = new Db.Entities.Language()
            {
                Name = "PHP",
                Description = "descriptionTwo",
                Category = category2
            };
            context.Languages.Add(language2);

            context.Operators.Add(new Db.Entities.Operator()
            {
                Name = "Plus",
                Description = "descriptionOne",
                Language = language1
            });

            context.Operators.Add(new Db.Entities.Operator()
            {
                Name = "Minus",
                Description = "descriptionTwo",
                Language = language2
            });

            context.SaveChanges();
        }

        [TearDown]
        public async override Task TearDown()
        {
            await using var context = await DbContext();
            context.Operators.RemoveRange(context.Operators);
            context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            //context.Comments.RemoveRange(context.Comments);
            context.SaveChanges();
            await base.TearDown();
        }

        protected static class Urls
        {
            public static string GetOperators(int? offset = null, int? limit = null)
            {

                if (offset is null && limit is null)
                    return $"/api/v1/operator";
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
                return $"/api/v1/operator?{queryString}";
            }

            public static string GetOperator(string id) => $"/api/v1/operator/{id}";

            public static string DeleteOperator(string id) => $"/api/v1/operator/{id}";

            public static string UpdateOperator(string id) => $"/api/v1/operator/{id}";

            public static string AddOperator() => $"/api/v1/operator";
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

        public async Task<int> GetExistedLanguageId()
        {
            await using var context = await DbContext();
            if (context.Languages.Count() == 0)
            {
                Db.Entities.Language language = new Db.Entities.Language()
                {
                    Name = "Test",
                    Description = "testDescription",
                    Category = new Db.Entities.Category()
                    {
                        Name = "Test",
                        Description = "testDescription"
                    }
                };
                context.Languages.Add(language);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var category = context1.Languages.AsEnumerable().First();
            return category.Id;
        }

        public async Task<int> GetExistedOperatorId()
        {
            await using var context = await DbContext();
            if (context.Operators.Count() == 0)
            {
                Db.Entities.Operator operator1 = new Db.Entities.Operator()
                {
                    Name = "test",
                    Description = "test",
                    Language = new Db.Entities.Language()
                    {
                        Name = "Test",
                        Description = "testDescription",
                        Category = new Db.Entities.Category()
                        {
                            Name = "Test",
                            Description = "testDescription"
                        }
                    }
                };              
            
                context.Operators.Add(operator1);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var category = context1.Operators.AsEnumerable().First();
            return category.Id;
        }

        public async Task<int> GetNotExistedCategoryId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Categories.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }

        public async Task<int> GetNotExistedLanguageId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Languages.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }

        public async Task<int> GetNotExistedOperatorId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Operators.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }

    }
}
