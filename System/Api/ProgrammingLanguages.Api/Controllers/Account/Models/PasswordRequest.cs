using AutoMapper;
using FluentValidation;
using ProgrammingLanguage.Web.Pages.Profile.Models;

namespace ProgrammingLanguages.Api.Controllers.Account.Models
{
    public class PasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class PasswordRequestValidator : AbstractValidator<PasswordRequest>
    {
        public PasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short.");
        }
    }

    public class PasswordRequestProfile : Profile
    {
        public PasswordRequestProfile()
        {
            CreateMap<PasswordRequest, PasswordModel>();
        }
    }

}
