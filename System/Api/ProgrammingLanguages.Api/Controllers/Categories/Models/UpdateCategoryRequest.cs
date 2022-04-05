using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.CategoryService.Models;

namespace ProgrammingLanguages.Api.Controllers.Categories.Models
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =string.Empty;
    }

    public class UpdateCategoryResponseValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(199).WithMessage("Name is long.");
            RuleFor(x => x.Description)
               .MaximumLength(2000).WithMessage("Description is long.");
        }
    }

    public class UpdateCategoryRequestProfile : Profile
    {
        public UpdateCategoryRequestProfile()
        {
            CreateMap<UpdateCategoryRequest, UpdateCategoryModel>();
        }
    }
}
