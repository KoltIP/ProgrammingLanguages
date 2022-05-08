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
        public async Task UpdateOperator_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var updateOperator = context.Operators.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id == id);
            Assert.IsNotNull(updateOperator);

            Assert.AreEqual(request.languageId, updateOperator?.LanguageId);
            Assert.AreEqual(request.Name, updateOperator?.Name);
            Assert.AreEqual(request.Description, updateOperator?.Description);
        }


        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidLanguageIds))]
        public async Task UpdateOperator_InvalidLanguage_Authenticated_BadRequest(int languageId)
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var request = AddOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateOperator_ValidLanguage_Authenticated_OkResponse()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task UpdateOperator_InvalidTitle_Authenticated_BadRequest(string title)
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task UpdateOperator_ValidTitle_Authenticated_OkResponse(string title)
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task UpdateOperator_InvalidDescription_Authenticated_BadRequest(string description)
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task UpdateOperator_ValidDescription_Authenticated_OkResponse(string description)
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task UpdateOperator_Unauthorized()
        {
            int id = await GetExistedOperatorId();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task UpdateOperator_Forbidden()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.UpdateOperator(id.ToString());

            var languageId = await GetExistedLanguageId();
            var request = UpdateOperatorRequest(languageId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
