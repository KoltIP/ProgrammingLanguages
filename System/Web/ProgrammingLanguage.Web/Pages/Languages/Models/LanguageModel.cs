using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Languages.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class LanguageModelValidator : AbstractValidator<LanguageModel>
    {
        public LanguageModelValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(256)
                .NotEmpty();
            RuleFor(v => v.CategoryId)
                .NotEmpty();
            RuleFor(v => v.Name)
                .MaximumLength(1024);
        }

        public Func<object, object, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LanguageModel>.CreateWithOptions((LanguageModel)model, x => x.IncludeProperties((string)propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

    


}
