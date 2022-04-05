using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.OperatorService.Models
{
    public class AddOperatorModel
    {
        public int LanguageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class AddOperatorResponseValidator : AbstractValidator<AddOperatorModel>
    {
        public AddOperatorResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description is long.");
        }
    }

    public class AddOperatorRequestProfile : Profile
    {
        public AddOperatorRequestProfile()
        {
            CreateMap<AddOperatorModel, Operator>();
        }
    }
}
