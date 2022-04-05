using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.CategoryService.Models;

namespace ProgrammingLanguages.Api.Controllers.Categories.Models
{
    public class AddCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
    }

    public class AddCategoryResponseValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(199).WithMessage("Name is long.");
            RuleFor(x => x.Description)
               .MaximumLength(200).WithMessage("Description is long.");
        }
    }

    public class AddCategoryRequestProfile : Profile
    {
        public AddCategoryRequestProfile()
        {
            CreateMap<AddCategoryRequest, AddCategoryModel>();
        }
    }

}
