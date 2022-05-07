using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Profile
{
    public partial class ProfileIntegrationTest
    {
        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task ChangeName_ValidParameters_Authenticated_OkResponse(string name)
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeName(accessToken, name);

            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task ChangeName_InvalidToken_Authenticated_BadRequest()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeName("InvalidAccessToken", Generator.ValidNames.First());

            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task ChangeName_InvalidName_Authenticated_BadRequest(string name)
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeName(accessToken, name);

            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangeName_Unauthorized()
        {
            await CheckTestUser();
            var url = Urls.ChangeName(await AuthenticateUser_ReadAndWriteLanguageScope(), Generator.ValidNames.First());
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task ChangeName_EmptyScope_Forbidden()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.ChangeName(await AuthenticateUser_ReadAndWriteLanguageScope(), Generator.ValidNames.First());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
