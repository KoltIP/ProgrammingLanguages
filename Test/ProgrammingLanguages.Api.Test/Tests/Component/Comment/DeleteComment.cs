using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public partial class CommentIntegrationTest
    {
        [Test]
        public async Task DeleteComment_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var commentId = await GetExistedCommentId();
            var url = Urls.DeleteComment(commentId);
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            await using var context = await DbContext();
            var newComment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(commentId));
            Assert.IsNull(newComment);
        }

        [Test]
        public async Task DeleteComment_NegativeParameters_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.DeleteComment(int.MaxValue);
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task DeleteComment_Unauthorized()
        {
            var url = Urls.DeleteComment(1);
            var response = await apiClient.Delete(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task DeleteComment_EmptyScope_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.DeleteComment(1);
            var response = await apiClient.Delete(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
