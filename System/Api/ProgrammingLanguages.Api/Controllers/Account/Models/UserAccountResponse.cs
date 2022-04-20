using AutoMapper;
using ProgrammingLanguages.UserAccount.Models;

namespace ProgrammingLanguages.Api.Controllers.Account.Models
{
    public class UserAccountResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserAccountResponseProfile : Profile
    {
        public UserAccountResponseProfile()
        {
            CreateMap<UserAccountModel, UserAccountResponse>();
        }
    }
}
