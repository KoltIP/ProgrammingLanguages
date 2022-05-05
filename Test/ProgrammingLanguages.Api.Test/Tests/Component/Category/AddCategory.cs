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
        public async Task AddCategory_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddCategory();
                       
            var request = AddCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            await using var context = await DbContext();
            var newLanguage = context.Categories.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(newLanguage);

            Assert.AreEqual(request.Name, newLanguage?.Name);
            Assert.AreEqual(request.Description, newLanguage?.Description);
        }


        //[Test]
        //public async Task AddLanguage_ValidAuthor_Authenticated_OkResponse()
        //{
        //    var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
        //    var url = Urls.AddCategory();

        //    var categoryId = await GetExistedCategoryId();
        //    var request = AddCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
        //    var response = await apiClient.PostJson(url, request, accessToken);
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //}

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task AddCategory_InvalidTitle_Authenticated_BadRequest(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task AddCategory_ValidTitle_Authenticated_OkResponse(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task AddCategory_InvalidDescription_Authenticated_BadRequest(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task AddCategory_ValidDescription_Authenticated_OkResponse(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task AddCategory_Unauthorized()
        {
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task AddCategory_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.AddCategory();

            var categoryId = await GetExistedCategoryId();
            var request = AddCategoryRequest(Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
