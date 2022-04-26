using ProgrammingLanguage.Web.Pages.Profile.Models;

namespace ProgrammingLanguage.Web.Pages.Profile.Services
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfile();
        Task<ErrorResponse> ChangeName(NameModel model);
        Task<ErrorResponse> ChangeEmail(EmailModel model);
        Task<ErrorResponse> ChangePassword(PasswordModel model);
    }
}
