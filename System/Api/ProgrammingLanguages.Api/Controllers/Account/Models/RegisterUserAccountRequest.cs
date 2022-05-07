using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.UserAccount.Models;

namespace ProgrammingLanguages.Api.Controllers.Account.Models
{
    public class RegisterUserAccountRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserAccountRequestValidator : AbstractValidator<RegisterUserAccountRequest>
    {
        public RegisterUserAccountRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("User name is required.")
                .MaximumLength(100).WithMessage("Name is long.");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .MaximumLength(100).WithMessage("Email is long.")
            .EmailAddress().WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short (minimum 3 simbols).");
        }
    }

    public class RegisterUserAccountRequestProfile : Profile
    {
        public RegisterUserAccountRequestProfile()
        {
            CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
        }
    }
}
