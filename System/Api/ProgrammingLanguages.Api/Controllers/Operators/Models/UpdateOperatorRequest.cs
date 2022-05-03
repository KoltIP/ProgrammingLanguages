using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Controllers.Operators.Models
{
    public class UpdateOperatorRequest
    {
        public int languageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateOperatorResponseValidator : AbstractValidator<UpdateOperatorRequest>
    {
        public UpdateOperatorResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");

            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description is long.");
        }
    }

    public class UpdateOperatorRequestProfile : Profile
    {
        public UpdateOperatorRequestProfile()
        {
            CreateMap<UpdateOperatorRequest, UpdateOperatorModel>();
        }
    }
}
