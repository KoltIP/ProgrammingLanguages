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
        public async Task ChangeEmail_ValidParameters_Authenticated_OkResponse()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeEmail(accessToken, Generator.ValidEmails.First());

            var response = await apiClient.Get(url, accessToken);

            var userId = (await userManager.FindByEmailAsync(Generator.ValidEmails.First())).Id;
            await using var context = await DbContext();
            var newUser = context.Users.FirstOrDefault(x => x.Id == userId);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(Generator.ValidEmails.First(), newUser?.Email);
        }

        [Test]
        public async Task ChangeEmail_InvalidToken_Authenticated_BadRequest()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeEmail("InvalidAccessToken", Generator.ValidEmails.First());

            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangeEmail_InvalidName_Authenticated_BadRequest()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.ChangeEmail(accessToken, Generator.InvalidEmails.First());

            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task ChangeEmail_Unauthorized()
        {
            await CheckTestUser();
            var url = Urls.ChangeEmail(await AuthenticateUser_ReadAndWriteLanguageScope(), Generator.ValidEmails.First());
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task ChangeEmail_EmptyScope_Forbidden()
        {
            await CheckTestUser();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.ChangeEmail(await AuthenticateUser_ReadAndWriteLanguageScope(), Generator.ValidEmails.First());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
