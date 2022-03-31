using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.LanguageService.Models
{
    public class AddLanguageModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AddLanguageResponseValidator : AbstractValidator<AddLanguageModel>
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
            CreateMap<AddLanguageModel, Language>();
        }
    }
}
