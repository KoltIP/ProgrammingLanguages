using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.CategoryService.Models
{
    public class UpdateCategoryModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
    }

    public class UpdateCategoryResponseValidator : AbstractValidator<AddCategoryModel>
    {
        public UpdateCategoryResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
            RuleFor(x => x.Description)
               .MaximumLength(2000).WithMessage("Description is long.");

        }
    }

    public class UpdateCategoryRequestProfile : Profile
    {
        public UpdateCategoryRequestProfile()
        {
            CreateMap<UpdateCategoryModel, Category>();
        }
    }
}
