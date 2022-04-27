using ProgrammingLanguage.Web.Pages.Auth;
using ProgrammingLanguage.Web.Pages.Auth.ForgotPsw;
using ProgrammingLanguage.Web.Pages.Auth.Registr;
using ProgrammingLanguage.Web.Pages.Profile.Models;

namespace ProgrammingLanguage.Web.Pages.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegistrErrorResponse> Registration(RegistrModel registrationModel);
        Task<bool> InspectEmail(string email);
        Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordModel forgotPassword);
    }
}
