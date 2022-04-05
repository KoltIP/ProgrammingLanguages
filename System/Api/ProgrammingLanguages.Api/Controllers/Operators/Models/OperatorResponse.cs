using AutoMapper;
using ProgrammingLanguages.OperatorService.Models;

namespace ProgrammingLanguages.Api.Controllers.Operators.Models
{
    public class OperatorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class OperatorRequestProfile : Profile
    {
        public OperatorRequestProfile()
        {
            CreateMap<OperatorModel, OperatorResponse>();
        }
    }
}
