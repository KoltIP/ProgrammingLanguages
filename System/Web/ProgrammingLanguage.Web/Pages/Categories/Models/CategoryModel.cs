using FluentValidation;

namespace ProgrammingLanguage.Web.Pages.Categories.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Name is required.")
                 .MaximumLength(200).WithMessage("Name is long.");
            RuleFor(x => x.Description)
               .MaximumLength(2000).WithMessage("Description is long.");
        }

        public Func<object, object, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CategoryModel>.CreateWithOptions((CategoryModel)model, x => x.IncludeProperties((string)propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
