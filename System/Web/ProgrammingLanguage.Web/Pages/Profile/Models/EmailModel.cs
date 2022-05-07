using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Profile.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
    }
    public class EmailModelValidator : AbstractValidator<EmailModel>
    {
        public EmailModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is empty.")
                .MaximumLength(100).WithMessage("Email is long.")
                .EmailAddress().WithMessage("Email is required.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EmailModel>.CreateWithOptions((EmailModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
