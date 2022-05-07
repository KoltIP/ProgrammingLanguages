using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Operators.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Operator
{
    public partial class OperatorIntegrationTest
    {
        [Test]
        public async Task GetOperators_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetOperators();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var operators_from_api = await response.ReadAsObject<IEnumerable<OperatorResponse>>();

            await using var context = await DbContext();
            var operators_from_db = context.Operators.AsEnumerable();

            Assert.AreEqual(operators_from_db.Count(), operators_from_api.Count());
        }

        //[Test]
        //public async Task GetOperators_NegativeParameters_OkResponse()
        //{
        //    var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
        //    var url = Urls.GetOperators(-1, -1);
        //    var response = await apiClient.Get(url, accessToken);
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        //    var operators_from_api = await response.ReadAsObject<IEnumerable<OperatorResponse>>();

        //    Assert.AreEqual(0, operators_from_api.Count());
        //}

        [Test]
        public async Task GetOperators_Unauthorized()
        {
            var url = Urls.GetOperators();
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetOperators_EmptyScope_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetOperators();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
