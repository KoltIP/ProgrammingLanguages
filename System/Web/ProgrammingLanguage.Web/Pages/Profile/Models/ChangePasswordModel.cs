using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Profile.Models
{
    public class ChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password is long.")                
                .MinimumLength(3).WithMessage("Password is short (minimum 8 characters).");
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ChangePasswordModel>.CreateWithOptions((ChangePasswordModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
