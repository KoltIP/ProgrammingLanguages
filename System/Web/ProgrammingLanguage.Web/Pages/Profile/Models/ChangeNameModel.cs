using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Profile.Models
{
    public class ChangeNameModel
    {
        public string Name { get; set; }
    }
    public class ChangeFullNameModelValidator : AbstractValidator<ChangeNameModel>
    {
        public ChangeFullNameModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("FullName is empty.")
                .MaximumLength(100).WithMessage("FullName is long.")
                .MinimumLength(1).WithMessage("FullName is short.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ChangeNameModel>.CreateWithOptions((ChangeNameModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
