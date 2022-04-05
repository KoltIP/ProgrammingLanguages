using AutoMapper;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.CategoryService.Models
{
    public class CategoryModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } =string.Empty;
    }

    public class CategoryModelProfile : Profile
    {
        public CategoryModelProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
