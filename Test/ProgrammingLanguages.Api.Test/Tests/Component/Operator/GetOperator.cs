using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Operators.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
    {
        [Test]
        public async Task GetOperator_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetOperator(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var operator_from_api = await response.ReadAsObject<OperatorResponse>();

            await using var context = await DbContext();
            var operator_from_db = context.Operators.FirstOrDefault(x => x.Id == id);

            Assert.AreEqual(operator_from_db.Id, operator_from_api.Id);
        }

        [Test]
        public async Task GetOperator_NegativeParameters_OkResponse()
        {
            int id = await GetNotExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetOperator((id).ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task GetOperator_Unauthorized()
        {
            int id = await GetExistedOperatorId();
            var url = Urls.GetOperator(id.ToString());
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetOperator_EmptyScope_Forbidden()
        {
            int id = await GetExistedOperatorId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetOperator(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
