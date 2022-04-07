using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.LanguageService.Models
{
    public class UpdateLanguageModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateLanguageResponseValidator : AbstractValidator<UpdateLanguageModel>
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
            CreateMap<UpdateLanguageModel, Language>();
        }
    }
}
