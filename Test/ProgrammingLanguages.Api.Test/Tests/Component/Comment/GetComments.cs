using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Comment.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public partial class CommentIntegrationTest
    {
        [Test]
        public async Task GetComments_ValidParameters_Authenticated_OkResponse()
        {
            var id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetComments(id);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var comments_from_api = await response.ReadAsObject<IEnumerable<CommentResponse>>();

            await using var context = await DbContext();
            var comments_from_db = context.Comments.AsEnumerable();

            Assert.AreEqual(comments_from_db.Count(), comments_from_api.Count());
        }

        [Test]
        public async Task GetComments_NegativeParameters_OkResponse()
        {
            var id = await GetNotExistedLanguageId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetComments(id);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var comments_from_api = await response.ReadAsObject<IEnumerable<CommentResponse>>();

            Assert.AreEqual(0, comments_from_api.Count());
        }

        [Test]
        public async Task GetComments_Unauthorized()
        {
            var id = await GetExistedLanguageId();
            var url = Urls.GetComments(id);
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetComments_EmptyScope_Forbidden()
        {
            var id = await GetExistedLanguageId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetComments(id);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
