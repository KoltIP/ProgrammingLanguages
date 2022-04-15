using AutoMapper;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.LanguageService.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public class LanguageModelProfile : Profile
    {
        public LanguageModelProfile()
        {
            CreateMap<Language, LanguageModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
