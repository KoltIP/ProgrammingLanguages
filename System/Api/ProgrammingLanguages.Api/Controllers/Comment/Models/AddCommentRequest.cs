using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.CommentService.Model;

namespace ProgrammingLanguages.Api.Controllers.Comment.Models
{
    public class AddCommentRequest
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
    public class AddCommentRequestValidator : AbstractValidator<AddCommentRequest>
    {
        public AddCommentRequestValidator()
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
    public class AddCommentRequestProfile : Profile
    {
        public AddCommentRequestProfile()
        {
            CreateMap<AddCommentRequest, AddCommentModel>();
        }
    }

}
