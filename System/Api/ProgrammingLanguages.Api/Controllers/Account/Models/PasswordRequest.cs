using AutoMapper;
using FluentValidation;
using ProgrammingLanguage.Web.Pages.Profile.Models;

namespace ProgrammingLanguages.Api.Controllers.Account.Models
{
    public class PasswordRequest
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
    public class PasswordRequestValidator : AbstractValidator<PasswordRequest>
    {
        public PasswordRequestValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(50).WithMessage("Password is long.")
                .MinimumLength(3).WithMessage("Password is short (minimum 3 characters).");
        }
    }

    public class PasswordRequestProfile : Profile
    {
        public PasswordRequestProfile()
        {
            CreateMap<PasswordRequest, PasswordModel>()
                .ForMember(d => d.NewPassword, a => a.MapFrom(s => s.NewPassword))
                .ForMember(d => d.OldPassword, a => a.MapFrom(s => s.OldPassword));
        }
    }


}
