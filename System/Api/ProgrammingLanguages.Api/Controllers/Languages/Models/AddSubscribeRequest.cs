using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.LanguageService.Model;

namespace ProgrammingLanguages.Api.Controllers.Languages.Models
{
    public class AddSubscribeRequest
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }

    public class AddSubscribeRequestValidator : AbstractValidator<AddSubscribeRequest>
    {
        public AddSubscribeRequestValidator()
        {            
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required");
            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("Programming language is required");
        }
    }
    public class AddSubscribeRequestProfile : Profile
    {
        public AddSubscribeRequestProfile()
        {
            CreateMap<AddSubscribeRequest, AddSubscribeModel>();
        }
    }
}
