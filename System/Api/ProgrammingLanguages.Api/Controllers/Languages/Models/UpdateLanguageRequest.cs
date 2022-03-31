using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Controllers.Languages.Models
{
    public class UpdateLanguageRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateLanguageResponseValidator : AbstractValidator<UpdateLanguageRequest>
    {
        public UpdateLanguageResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description is long.");
        }
    }

    public class UpdateLanguageRequestProfile : Profile
    {
        public UpdateLanguageRequestProfile()
        {
            CreateMap<UpdateLanguageRequest, UpdateLanguageModel>();
        }
    }
}
