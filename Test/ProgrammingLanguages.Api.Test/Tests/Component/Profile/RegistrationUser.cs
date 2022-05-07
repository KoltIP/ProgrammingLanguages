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
        public async Task RegisterUser_ValidParameters_UserIsNotNull()
        {
            var url = Urls.Register();

            var request = RegisterUserAccountRequest(Generator.ValidNames.First(), Generator.ValidEmails.First(), Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request);

            var userId = (await userManager.FindByEmailAsync(Generator.ValidEmails.First())).Id;
            await using var context = await DbContext();
            var newUser = context.Users.FirstOrDefault(x => x.Id == userId);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(request.Email, newUser?.Email);

        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task RegisterUser_InvalidName_BadRequest(string name)
        {
            var url = Urls.Register();

            var request = RegisterUserAccountRequest(name, Generator.ValidEmails.First(), Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidEmails))]
        public async Task RegisterUser_InvalidEmail_BadRequest(string email)
        {
            var url = Urls.Register();

            var request = RegisterUserAccountRequest(Generator.ValidNames.First(), email, Generator.ValidPasswords.First());
            var response = await apiClient.PostJson(url, request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidPasswords))]
        public async Task RegisterUser_InvalidPassword_BadRequest(string password)
        {
            var url = Urls.Register();

            var request = RegisterUserAccountRequest(Generator.ValidNames.First(), Generator.ValidEmails.First(), password);
            var response = await apiClient.PostJson(url, request);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}
