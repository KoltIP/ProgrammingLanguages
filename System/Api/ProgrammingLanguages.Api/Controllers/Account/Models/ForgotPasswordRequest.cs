using AutoMapper;
using FluentValidation;
using ProgrammingLanguage.Web.Pages.Auth.ForgotPsw;

namespace ProgrammingLanguages.Api.Controllers.Account.Models
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
    public class ResetPasswordUserRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ResetPasswordUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .MinimumLength(8).WithMessage("Email is short")
                .MaximumLength(100).WithMessage("Email is long.")
                .EmailAddress().WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short.");
        }
    }
    public class ForgotPasswordRequestProfile : Profile
    {
        public ForgotPasswordRequestProfile()
        {
            CreateMap<ForgotPasswordRequest, ForgotPasswordModel>();
        }
    }

}
