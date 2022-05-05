using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common;
using ProgrammingLanguages.Shared.Common.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    [TestFixture]
    public partial class CommentIntegrationTest : ComponentTest
    {
        const string EmailTestUser = "test@test.ru";
        const string PasswordTestUser = "test";

        [SetUp]
        public async Task SetUp()
        {
            await using var context = await DbContext();

            context.Comments.RemoveRange(context.Comments);
            context.Operators.RemoveRange(context.Operators);
            context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            
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

            context.Languages.Add(new Db.Entities.Language()
            {
                Name = "Java",
                Description = "descriptionOne",
                Category = category1
            });

            context.Languages.Add(new Db.Entities.Language()
            {
                Name = "PHP",
                Description = "descriptionTwo",
                Category = category2
            });

            context.SaveChanges();
        }

        [TearDown]
        public async override Task TearDown()
        {
            await using var context = await DbContext();
            context.Comments.RemoveRange(context.Comments);
            context.Operators.RemoveRange(context.Operators);
            context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();
            await base.TearDown();
        }

        protected static class Urls
        {
            public static string GetComments(int languageId) => $"/api/v1/comments/get/many/{languageId}";

            public static string GetComment(int id) => $"/api/v1/comments/get/one/{id}";

            public static string DeleteComment(int id) => $"/api/v1/comments/delete/{id}";

            public static string UpdateComment(int commentId) => $"/api/v1/comments/update/{commentId}";

            public static string AddComment() => $"/api/v1/comments/add";
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

        public async Task<int> GetExistedCommentId()
        {
            await using var context = await DbContext();
            if (context.Comments.Count() == 0)
            {
                Db.Entities.Comment comment = new Db.Entities.Comment()
                {
                    Content = "Test",
                    UserId = await GetExistedUserId(),
                    LanguageId = await GetExistedLanguageId()
                };
                context.Comments.Add(comment);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var comment1 = context1.Comments.AsEnumerable().First();
            return comment1.Id;
        }

        public async Task<int> GetNotExistedCommentId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Comments.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }

        public async Task<Guid> GetExistedUserId()
        {
            return (await userManager.FindByEmailAsync(EmailTestUser)).Id;
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

        public async Task<int> GetNotExistedLanguageId()
        {
            await using var context = await DbContext();
            var maxExistedLanguageId = context.Languages.Max(x => x.Id);

            return maxExistedLanguageId + 1;
        }
    }

}
