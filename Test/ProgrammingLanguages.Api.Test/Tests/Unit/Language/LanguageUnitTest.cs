using ProgrammingLanguages.Api.Test.Common;
using ProgrammingLanguages.Db.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Unit.Language
{
    public partial class LanguageUnitTest : UnitTest
    {
        public async Task<int> GetExistedCategoryId()
        {
            await using var context = await DbContext();
            if (context.Categories.Count() == 0)
            {
                Category category1 = new Category()
                {
                    Name = "TestCategory1",
                    Description = "testCategoryDescription1"
                };
                context.Categories.Add(category1);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var category = context1.Categories.AsEnumerable().First();
            return category.Id;
        }

        public async Task<int> GetExistedLanguageId()
        {
            await using var context = await DbContext();
            if (context.Languages.Count() == 0)
            {
                Db.Entities.Language language = new Db.Entities.Language()
                {
                    CategoryId = await GetExistedCategoryId(),
                    Name = "TestLanguage1",
                    Description = "testLanguageDescription1"
                };
                context.Languages.Add(language);
                context.SaveChanges();
            }

            await using var context1 = await DbContext();
            var language1 = context1.Languages.AsEnumerable().First();
            return language1.Id;
        }


        public async Task<Db.Entities.Language> GetLanguageById(int id)
        {
            await using var context = await DbContext();
            var language = context.Languages.FirstOrDefault(x => x.Id == id);
            return language;
        }

        protected async override Task ClearDb()
        {
            await using var context = await DbContext();
            context.Languages.RemoveRange(context.Languages);
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();
        }
    }
}
