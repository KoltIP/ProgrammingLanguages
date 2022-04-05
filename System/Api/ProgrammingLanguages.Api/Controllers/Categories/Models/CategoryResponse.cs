using AutoMapper;
using ProgrammingLanguages.CategoryService.Models;

namespace ProgrammingLanguages.Api.Controllers.Categories.Models
{
    public class CategoryResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
    }

    public class CategoryRequestProfile : Profile
    {
        public CategoryRequestProfile()
        {
            CreateMap<CategoryModel, CategoryResponse>();
        }
    }
}
