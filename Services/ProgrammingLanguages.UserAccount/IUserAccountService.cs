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

        // .. Также здесь можно разместить методы для изменения данных учетной записи, восстановления и смены пароля,
        // подтверждения электронной почты, установки телефона и его подтверждения и т.д.
        // .. Но это уже на самостоятельно.
        // .. Удачи! Я в вас верю!  :)
    }
}
