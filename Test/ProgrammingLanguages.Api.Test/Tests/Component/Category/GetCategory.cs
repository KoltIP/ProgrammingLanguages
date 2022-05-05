using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Categories.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        [Test]
        public async Task GetCategory_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetCategory(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var category_from_api = await response.ReadAsObject<CategoryResponse>();

            await using var context = await DbContext();
            var category_from_db = context.Categories.FirstOrDefault(x => x.Id == id);

            Assert.AreEqual(category_from_db.Id, category_from_api.Id);
        }

        [Test]
        public async Task GetCategory_NegativeParameters_OkResponse()
        {
            int id = await GetNotExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetCategory((id).ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task GetCategory_Unauthorized()
        {
            int id = await GetExistedCategoryId();
            var url = Urls.GetCategory(id.ToString());
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetCategory_EmptyScope_Forbidden()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetCategory(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
