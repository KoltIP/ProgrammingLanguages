using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.CommentService.Model;

namespace ProgrammingLanguages.Api.Controllers.Comment.Models
{
    public class UpdateCommentRequest
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
    public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MaximumLength(1000).WithMessage("Content is long");
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required");
            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("Programming language is required");
        }
    }
    public class UpdateCommentRequestProfile : Profile
    {
        public UpdateCommentRequestProfile()
        {
            CreateMap<UpdateCommentRequest, UpdateCommentModel>();
        }
    }

}
