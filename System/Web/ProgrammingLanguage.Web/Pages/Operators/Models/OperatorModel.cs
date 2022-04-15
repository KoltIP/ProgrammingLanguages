using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Operators.Models
{
    public class OperatorModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        public string Language { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class OperatorModelValidator : AbstractValidator<OperatorModel>
    {
        public OperatorModelValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(256)
                .NotEmpty();
            RuleFor(v => v.LanguageId)
                .NotEmpty();
            RuleFor(v => v.Name)
                .MaximumLength(1024);
        }

        public Func<object, object, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<OperatorModel>.CreateWithOptions((OperatorModel)model, x => x.IncludeProperties((string)propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
