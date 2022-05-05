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
        public async Task AddComment_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddComment();

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = AddCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var newComment = context.Comments.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(newComment);

            Assert.AreEqual(request.Content, newComment?.Content);
            Assert.AreEqual(request.UserId, newComment?.UserId);
            Assert.AreEqual(request.LanguageId, newComment?.LanguageId);
        }

        [Test]
        public async Task AddComment_InvalidLanguageId_Authenticated_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddComment();

            var languageId = await GetNotExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = AddCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task AddComment_InvalidUserId_Authenticated_BadRequest()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddComment();

            var languageId = await GetExistedLanguageId();
            var userId = Guid.NewGuid();
            var request = AddCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidComments))]
        public async Task AddComment_InvalidComment_Authenticated_BadRequest(string comment)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteLanguageScope();
            var url = Urls.AddComment();

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = AddCommentRequest(comment, userId, languageId);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task AddComment_Unauthorized()
        {
            var url = Urls.AddComment();

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = AddCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task AddComment_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.AddComment();

            var languageId = await GetExistedLanguageId();
            var userId = await GetExistedUserId();
            var request = AddCommentRequest(Generator.ValidComments.First(), userId, languageId);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }

}
