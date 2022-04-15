using ProgrammingLanguage.Web.Pages.Auth;

namespace ProgrammingLanguage.Web.Pages.Auth.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}
