using NUnit.Framework;
using ProgrammingLanguages.LanguageService;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Unit.Language
{
    [TestFixture]
    public partial class LanguageUnitTest
    {
        [Test]
        public async Task AddLanguage_ValidParameters_Success()
        {
            var languageService = services.Get<ILanguageService>();

            //var languageId = await GetExistedLanguageId();
            var categoryId = await GetExistedCategoryId();

            var model = AddLanguageModel(categoryId, "testname", "testDescription");

            await languageService.AddLanguage(model);

            //var resultLanguage = await GetLanguageById(languageId);

            //Assert.IsNotNull(resultLanguage);

            //Assert.AreEqual(model.CategoryId, resultLanguage.CategoryId);
            //Assert.AreEqual(model.Name, resultLanguage.Name);
            //Assert.AreEqual(model.Description, resultLanguage.Description);
        }
    }
}
