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
                .NotEmpty().WithMessage("User name is required.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password is long.");
        }
    }
}
