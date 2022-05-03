using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProgrammingLanguages.Api.Controllers.Comment.Models;
using ProgrammingLanguages.Api.Controllers.Languages;
using ProgrammingLanguages.CommentService;
using ProgrammingLanguages.CommentService.Model;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Shared.Common.Security;

namespace ProgrammingLanguages.Api.Controllers.Comment
{
    [Route("api/v{version:apiVersion}/comments")]
    [ApiController]
    [ApiVersion("1.0")]
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<CommentController> logger;
        private readonly ICommentService commentService;

        public CommentController(IMapper mapper, ILogger<CommentController> logger, ICommentService commentService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.commentService = commentService;
        }


        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("get/many/{languageId}")]
        public async Task<IEnumerable<CommentResponse>> GetCommentByLanguageId([FromRoute]  int languageId)
        {
            var comments = await commentService.GetCommentsByLanguageId(languageId);
            var response = mapper.Map<IEnumerable<CommentResponse>>(comments);
            return response;
        }

        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("get/one/{id}")]
        public async Task<CommentResponse> GetCommentByCommentId([FromRoute] int id)
        {
            var comment = await commentService.GetCommentByCommentId(id);
            var response = mapper.Map<CommentResponse>(comment);
            return response;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPost("add")]
        public async Task<CommentResponse> AddCommentAsync([FromBody] AddCommentRequest request)
        {
            var model = mapper.Map<AddCommentModel>(request);
            var comment = await commentService.AddComment(model);
            var result = mapper.Map<CommentResponse>(comment);
            return result;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPut("update/{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] int commentId, [FromBody] UpdateCommentRequest request)
        {
            var model = mapper.Map<UpdateCommentModel>(request);
            await commentService.UpdateComment(commentId, model);
            return Ok();
        }



        [Authorize(AppScopes.LanguageWrite)]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] int id)
        {

            await commentService.DeleteComment(id);
            return Ok();
        }

        ///Subscribe
    }
}
