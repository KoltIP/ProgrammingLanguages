using NUnit.Framework;
using ProgrammingLanguages.Api.Controllers.Comment.Models;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public partial class CommentIntegrationTest
    {
        [Test]
        public async Task GetComment_ValidParameters_Authenticated_OkResponse()
        {
            var id = await GetExistedCommentId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetComment(id);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var comments_from_api = await response.ReadAsObject<CommentResponse>();

            Assert.AreEqual(await GetExistedCommentId(), comments_from_api.Id);
        }

        [Test]
        public async Task GetComment_NegativeParameters_NoContent()
        {
            //var id = await GetNotExistedCommentId();
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.GetComment(-1);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task GetComment_Unauthorized()
        {
            var id = await GetExistedCommentId();
            var url = Urls.GetComment(id);
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetComment_EmptyScope_Forbidden()
        {
            var id = await GetExistedCommentId();
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetComment(id);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
