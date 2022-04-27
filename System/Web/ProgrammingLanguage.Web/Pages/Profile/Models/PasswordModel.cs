using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Profile.Models
{
    public class PasswordModel
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
    public class PasswordModelValidator : AbstractValidator<PasswordModel>
    {
        public PasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short (minimum 3 characters).");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<PasswordModel>.CreateWithOptions((PasswordModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }



}
