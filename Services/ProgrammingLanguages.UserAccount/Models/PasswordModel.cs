using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.UserAccount.Models
{
    public class PasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class PasswordModelValidator : AbstractValidator<PasswordModel>
    {
        public PasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password is long.")
                .MinimumLength(8).WithMessage("Password is short.");
        }
    }

}
