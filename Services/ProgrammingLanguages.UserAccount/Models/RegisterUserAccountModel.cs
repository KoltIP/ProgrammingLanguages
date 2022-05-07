using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.UserAccount.Models
{
    public class RegisterUserAccountModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
    {
        public RegisterUserAccountModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name is long.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email email is required.")
               .MaximumLength(100).WithMessage("Email name is long.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short (minimum 3 simbols).");
        }
    }
}
