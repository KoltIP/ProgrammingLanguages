using AutoMapper;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.OperatorService.Models
{
    public class OperatorModel
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class OperatorModelProfile : Profile
    {
        public OperatorModelProfile()
        {
            CreateMap<Operator, OperatorModel>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language.Name));
        }
    }
}
