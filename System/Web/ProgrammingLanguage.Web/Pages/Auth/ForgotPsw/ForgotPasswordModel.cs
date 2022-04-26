using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Auth.ForgotPsw
{
    public class ForgotPasswordModel
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordModelValidator : AbstractValidator<ForgotPasswordModel>
    {
        public ForgotPasswordModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is empty.")
                .MaximumLength(100).WithMessage("Email is long.")
                .MinimumLength(5).WithMessage("Email is short")
                .EmailAddress().WithMessage("Email is required.");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ForgotPasswordModel>.CreateWithOptions((ForgotPasswordModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
