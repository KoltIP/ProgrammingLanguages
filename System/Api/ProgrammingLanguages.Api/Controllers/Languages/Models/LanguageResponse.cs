using AutoMapper;
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Controllers.Languages.Models
{
    public class LanguageResponse
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class LanguageRequestProfile : Profile
    {
        public LanguageRequestProfile()
        {
            CreateMap<LanguageModel, LanguageResponse>();
        }
    }

}
