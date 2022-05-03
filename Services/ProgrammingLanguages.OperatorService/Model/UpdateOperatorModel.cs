using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.OperatorService.Models;

namespace ProgrammingLanguages.LanguageService.Models
{
    public class UpdateOperatorModel
    {
        public int LanguageId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateOperatorResponseValidator : AbstractValidator<UpdateOperatorModel>
    {
        public UpdateOperatorResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");

            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description is long.");
        }
    }

    public class UpdateOperatorRequestProfile : Profile
    {
        public UpdateOperatorRequestProfile()
        {
            CreateMap<UpdateOperatorModel, Operator>();
        }
    }
}
