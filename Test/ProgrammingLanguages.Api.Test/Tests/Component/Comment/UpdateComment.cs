using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ProgrammingLanguages.Api.Test.Common.Extensions;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public partial class CommentIntegrationTest
    {
        [Test]
        public async Task UpdateComment_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var idComment = await GetExistedCommentId();
            var url = Urls.UpdateComment(idComment);

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var newComment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(idComment));
            Assert.IsNotNull(newComment);

            Assert.AreEqual(request.Content, newComment?.Content);
            Assert.AreEqual(request.UserId, newComment?.UserId);
            Assert.AreEqual(request.LanguageId, newComment?.LanguageId);
        }

        [Test]
        public async Task UpdateComment_InvalidCommentId_Authenticated_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateComment(int.MaxValue);

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateComment_InvalidLanguageId_Authenticated_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateComment(await GetExistedCommentId());

            var languageId = await GetNotExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateComment_InvalidUserId_Authenticated_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateComment(await GetExistedCommentId());

            var languageId = await GetExistedLanguageId();
            var userId = Guid.NewGuid();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidComments))]
        public async Task UpdateComment_InvalidComment_Authenticated_BadRequest(string comment)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.UpdateComment(await GetExistedCommentId());

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(comment, userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task UpdateComment_Unauthorized()
        {
            var url = Urls.UpdateComment(await GetExistedCommentId());

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task UpdateComment_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.UpdateComment(await GetExistedCommentId());

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = UpdateCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PutJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
