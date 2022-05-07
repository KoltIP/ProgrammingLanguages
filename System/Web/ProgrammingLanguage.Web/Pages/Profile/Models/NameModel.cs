using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Profile.Models
{
    public class NameModel
    {
        public string Name { get; set; }
    }
    public class NameModelValidator : AbstractValidator<NameModel>
    {
        public NameModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("FullName is empty.")
                .MaximumLength(100).WithMessage("FullName is long.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<NameModel>.CreateWithOptions((NameModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
