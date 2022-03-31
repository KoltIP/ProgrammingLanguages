using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Controllers.Languages.Models
{
    public class AddLanguageRequest
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AddLanguageResponseValidator : AbstractValidator<AddLanguageRequest>
    {
        public AddLanguageResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description is long.");
        }
    }

    public class AddLanguageRequestProfile : Profile
    {
        public AddLanguageRequestProfile()
        {
            CreateMap<AddLanguageRequest, AddLanguageModel>();
        }
    }
}
