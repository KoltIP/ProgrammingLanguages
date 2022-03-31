using FluentValidation;

namespace ProgrammingLanguages.LanguageService.Models
{
    public class AddLanguageModel
    {
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
}
