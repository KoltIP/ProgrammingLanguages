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
        public async Task GetLanguage_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetLanguage(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var language_from_api = await response.ReadAsObject<LanguageResponse>();

            await using var context = await DbContext();
            var language_from_db = context.Languages.FirstOrDefault(x => x.Id == id);

            Assert.AreEqual(language_from_db.Id, language_from_api.Id);
        }

        [Test]
        public async Task GetLanguage_NegativeParameters_OkResponse()
        {
            int id = await GetNotExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetLanguage((id).ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task GetLanguage_Unauthorized()
        {
            int id = await GetExistedLanguageId();
            var url = Urls.GetLanguage(id.ToString());
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetLanguage_EmptyScope_Forbidden()
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetLanguage(id.ToString());
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }

    }
}
