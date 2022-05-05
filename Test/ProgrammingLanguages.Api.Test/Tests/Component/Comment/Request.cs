using ProgrammingLanguages.Api.Controllers.Comment.Models;
using System;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Comment
{
    public partial class CommentIntegrationTest
    {
        public static AddCommentRequest AddCommentRequest(string content, Guid userId, int languageId)
        {
            return new AddCommentRequest()
            {
                Content = content,
                UserId = userId,
                LanguageId = languageId
            };
        }

        public static UpdateCommentRequest UpdateCommentRequest(string content, Guid userId, int languageId)
        {
            return new UpdateCommentRequest()
            {
                Content = content,
                UserId = userId,
                LanguageId = languageId
            };
        }
    }
}
