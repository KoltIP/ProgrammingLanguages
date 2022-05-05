using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Categories.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        [Test]
        public async Task GetCategories_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var categories_from_api = await response.ReadAsObject<IEnumerable<CategoryResponse>>();

            await using var context = await DbContext();
            var categories_from_db = context.Categories.AsEnumerable();

            Assert.AreEqual(categories_from_db.Count(), categories_from_api.Count());
        }

        [Test]
        public async Task GetCategories_NegativeParameters_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetCategories(-1, -1);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var categories_from_api = await response.ReadAsObject<IEnumerable<CategoryResponse>>();

            Assert.AreEqual(0, categories_from_api.Count());
        }

        [Test]
        public async Task GetCategories_Unauthorized()
        {
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetCategories_EmptyScope_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
