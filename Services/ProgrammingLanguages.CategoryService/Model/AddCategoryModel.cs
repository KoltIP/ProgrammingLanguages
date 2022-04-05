using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.CategoryService.Models
{
    public class AddCategoryModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
    }

    public class AddCategoryResponseValidator : AbstractValidator<AddCategoryModel>
    {
        public AddCategoryResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(99).WithMessage("Name is long.");
            RuleFor(x => x.Description)
               .MaximumLength(2000).WithMessage("Description is long.");
        }
    }

    public class AddCategoryRequestProfile : Profile
    {
        public AddCategoryRequestProfile()
        {
            CreateMap<AddCategoryModel, Category>();
        }
    }
}
