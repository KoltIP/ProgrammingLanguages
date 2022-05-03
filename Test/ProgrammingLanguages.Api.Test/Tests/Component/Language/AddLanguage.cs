using ProgrammingLanguages.Api.Test.Common.Extensions;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Language
{
    public partial class LanguageIntegrationTest
    {
        [Test]
        public async Task AddBook_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var newLanguage = context.Languages.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(newLanguage);

            Assert.AreEqual(request.CategoryId, newLanguage?.CategoryId);
            Assert.AreEqual(request.Name, newLanguage?.Name);
            Assert.AreEqual(request.Description, newLanguage?.Description);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidCategoryIds))]
        public async Task AddBook_InvalidAuthor_Authenticated_BadRequest(int categoryId)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task AddBook_ValidAuthor_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task AddBook_InvalidTitle_Authenticated_BadRequest(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task AddBook_ValidTitle_Authenticated_OkResponse(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task AddBook_InvalidDescription_Authenticated_BadRequest(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task AddBook_ValidDescription_Authenticated_OkResponse(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task AddBook_Unauthorized()
        {
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task AddBook_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.AddLanguage();

            var categoryId = await GetExistedCategoryId();
            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
