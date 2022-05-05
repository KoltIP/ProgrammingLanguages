using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
    {
        [Test]
        public async Task DeleteOperator_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.DeleteOperator(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeleteOperator_NegativeParameters_OkResponse()
        {
            int id = await GetNotExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.DeleteOperator(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task DeleteOperator_Unauthorized()
        {
            int id = await GetExistedOperatorId();
            var url = Urls.DeleteOperator(id.ToString());
            var response = await apiClient.Delete(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task DeleteOperator_EmptyScope_Forbidden()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.DeleteOperator(id.ToString());
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
