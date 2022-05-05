using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Category
{
    public partial class CategoryIntegrationTest
    {
        [Test]
        public async Task UpdateCategory_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var updateLanguage = context.Categories.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id == id);
            Assert.IsNotNull(updateLanguage);

            Assert.AreEqual(request.Name, updateLanguage?.Name);
            Assert.AreEqual(request.Description, updateLanguage?.Description);
        }



        [Test]
        public async Task UpdateCategory_ValidAuthor_Authenticated_OkResponse()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task UpdateCategory_InvalidTitle_Authenticated_BadRequest(string title)
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task UpdateCategory_ValidTitle_Authenticated_OkResponse(string title)
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task UpdateCategory_InvalidDescription_Authenticated_BadRequest(string description)
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task UpdateCategory_ValidDescription_Authenticated_OkResponse(string description)
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task UpdateCategory_Unauthorized()
        {
            int id = await GetExistedCategoryId();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task UpdateCategory_Forbidden()
        {
            int id = await GetExistedCategoryId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.UpdateCategory(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
