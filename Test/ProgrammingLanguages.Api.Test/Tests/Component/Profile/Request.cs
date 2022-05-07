using ProgrammingLanguages.Api.Controllers.Account.Models;

namespace ProgrammingLanguages.Api.Test.Tests.Component.Profile
{
    public partial class ProfileIntegrationTest
    {
        public static RegisterUserAccountRequest RegisterUserAccountRequest(string name, string email, string password)
        {
            return new RegisterUserAccountRequest()
            {
                Name = name,
                Email = email,
                Password = password
            };
        }

        public static PasswordRequest ChangePasswordRequest(string oldPassword, string newPassword)
        {
            return new PasswordRequest()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword
            };
        }

    }
}
