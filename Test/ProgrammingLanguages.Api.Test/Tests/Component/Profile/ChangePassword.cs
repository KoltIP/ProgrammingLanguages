using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Profile
{
    public partial class ProfileIntegrationTest
    {
        const string OldPassword = "password";

        [Test]
        public async Task ChangePassword_ValidParameters_OkResponse()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangePassword(accessToken);

            var request = ChangePasswordRequest(OldPassword, Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request, accessToken);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidPasswords))]
        public async Task ChangePassword_InvalidNewPassword_BadRequest(string password)
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangePassword(accessToken);

            var request = ChangePasswordRequest(OldPassword, password);
            var response = await apiClient.PostJson(url, request, accessToken);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangePassword_InvalidOldPassword_BadRequest()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangePassword(accessToken);

            var request = ChangePasswordRequest("*******", Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request, accessToken);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangePassword_Unauthorized()
        {
            await CheckTestUser();
            var url = Urls.ChangePassword(await AuthenticateUser_ReadAndWriteLanguageScope());
            var request = ChangePasswordRequest(OldPassword, Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task ChangePasswordUser_EmptyScope_Forbidden()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.ChangePassword(await AuthenticateUser_ReadAndWriteLanguageScope());
            var request = ChangePasswordRequest(OldPassword, Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
