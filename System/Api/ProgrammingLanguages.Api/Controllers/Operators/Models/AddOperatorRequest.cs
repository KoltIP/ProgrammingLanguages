using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.OperatorService.Models;

namespace ProgrammingLanguages.Api.Controllers.Operators.Models
{
   
        public class AddOperatorRequest
        {
            public int LanguageId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
        }

        public class AddLanguageResponseValidator : AbstractValidator<AddOperatorRequest>
        {
            public AddLanguageResponseValidator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(50).WithMessage("Name is long.");

                RuleFor(x => x.Description)
                    .MaximumLength(50).WithMessage("Description is long.");
            }
        }

        public class AddLanguageRequestProfile : Profile
        {
            public AddLanguageRequestProfile()
            {
                CreateMap<AddOperatorRequest, AddOperatorModel>();
            }
        }
}

