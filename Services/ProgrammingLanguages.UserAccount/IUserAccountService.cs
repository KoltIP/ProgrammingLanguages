using ProgrammingLanguages.UserAccount.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.UserAccount
{
    public interface IUserAccountService
    {
        Task<UserAccountModel> Create(RegisterUserAccountModel model);
        Task Delete(string model);
        Task ConfirmEmail(Guid guid, string code);
        Task<bool> InspectConfirmEmail(string email);

        Task<UserAccountModel> GetUser(string token);


    }
}
