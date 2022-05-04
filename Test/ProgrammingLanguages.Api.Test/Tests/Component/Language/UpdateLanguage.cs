using NUnit.Framework;
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
        public async Task UpdateLanguage_ValidParameters_Authenticated_OkResponse()
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var updateLanguage = context.Languages.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault(x=>x.Id==id);
            Assert.IsNotNull(updateLanguage);

            Assert.AreEqual(request.CategoryId, updateLanguage?.CategoryId);
            Assert.AreEqual(request.Name, updateLanguage?.Name);
            Assert.AreEqual(request.Description, updateLanguage?.Description);
        }
        

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidCategoryIds))]
        public async Task UpdateLanguage_InvalidAuthor_Authenticated_BadRequest(int categoryId)
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var request = AddLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateLanguage_ValidAuthor_Authenticated_OkResponse()
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task UpdateLanguage_InvalidTitle_Authenticated_BadRequest(string title)
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidNames))]
        public async Task UpdateLanguage_ValidTitle_Authenticated_OkResponse(string title)
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, title, Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidDescriptions))]
        public async Task UpdateLanguage_InvalidDescription_Authenticated_BadRequest(string description)
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.ValidDescriptions))]
        public async Task UpdateLanguage_ValidDescription_Authenticated_OkResponse(string description)
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), description);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task UpdateLanguage_Unauthorized()
        {
            int id = await GetExistedLanguageId();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task UpdateLanguage_Forbidden()
        {
            int id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.UpdateLanguage(id.ToString());

            var categoryId = await GetExistedCategoryId();
            var request = UpdateLanguageRequest(categoryId, Generator.ValidNames.First(), Generator.ValidDescriptions.First());
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
