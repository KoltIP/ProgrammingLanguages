using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Languages.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Language
{
    public partial class LanguageIntegrationTest
    {
        [Test]
        public async Task GetLanguages_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetLanguages();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var languages_from_api = await response.ReadAsObject<IEnumerable<LanguageResponse>>();

            await using var context = await DbContext();
            var languages_from_db = context.Languages.AsEnumerable();

            Assert.AreEqual(languages_from_db.Count(), languages_from_api.Count());
        }

        [Test]
        public async Task GetLanguages_NegativeParameters_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetLanguages(0, 0);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var languages_from_api = await response.ReadAsObject<IEnumerable<LanguageResponse>>();

            Assert.AreEqual(0, languages_from_api.Count());
        }

        [Test]
        public async Task GetLanguages_Unauthorized()
        {
            var url = Urls.GetLanguages();
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetLanguages_EmptyScope_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetLanguages();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
