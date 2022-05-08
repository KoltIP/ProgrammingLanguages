using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
    {
        [Test]
        public async Task AddOperator_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var newLanguage = context.Operators.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(newLanguage);

            Assert.AreEqual(request.LanguageId, newLanguage?.LanguageId);
            Assert.AreEqual(request.Name, newLanguage?.Name);
            Assert.AreEqual(request.Description, newLanguage?.Description);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidLanguageIds))]
        public async Task AddOperator_InvalidLanguage_Authenticated_BadRequest(int languageId)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task AddOperator_ValidOperator_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task AddOperator_InvalidTitle_Authenticated_BadRequest(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedCategoryId();
            var request = AddOperatorRequest(languageId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task AddOperator_ValidTitle_Authenticated_OkResponse(string title)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task AddOperator_InvalidDescription_Authenticated_BadRequest(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task AddOperator_ValidDescription_Authenticated_OkResponse(string description)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), description);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task AddOperator_Unauthorized()
        {
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task AddOperator_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.AddOperator();

            var languageId = await GetExistedLanguageId();
            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
