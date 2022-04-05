using AutoMapper;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.OperatorService.Models
{
    public class OperatorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class OperatorModelProfile : Profile
    {
        public OperatorModelProfile()
        {
            CreateMap<Operator, OperatorModel>();
        }
    }
}
