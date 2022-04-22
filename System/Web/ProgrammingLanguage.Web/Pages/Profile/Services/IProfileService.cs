using ProgrammingLanguage.Web.Pages.Profile.Models;

namespace ProgrammingLanguage.Web.Pages.Profile.Services
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfile();
        Task ChangeName(ChangeNameModel model);
        Task<ErrorResponse> ChangeProfileEmail(ChangeEmailModel model);
        Task<ErrorResponse> ChangePassword(ChangePasswordModel model);
    }
}
