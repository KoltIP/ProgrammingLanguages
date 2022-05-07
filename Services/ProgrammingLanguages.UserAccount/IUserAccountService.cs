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
        Task ConfirmEmail(string email, string code);
        Task<bool> InspectEmail(string email);
        Task<UserAccountModel> GetUser(string token);
        Task ChangeName(string token, string name);
        Task ChangeEmail(string token, string email);
        Task ChangePassword(string token, PasswordModel password);
        Task ConfirmForgotPassword(string email, string code, string password);
        Task ForgotPassword(ForgotPasswordModel model);
        //Task<DateTime> LifetimeAccessToken(string token);
    }
}
