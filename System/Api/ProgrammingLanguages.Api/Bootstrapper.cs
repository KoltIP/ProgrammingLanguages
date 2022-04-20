using ProgrammingLanguages.CategoryService;
using ProgrammingLanguages.EmailService;
using ProgrammingLanguages.LanguageService;
using ProgrammingLanguages.OperatorService;
using ProgrammingLanguages.RabbitMqService;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.UserAccount;

namespace ProgrammingLanguages.Api
{
    public static class Bootstrapper
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services
                .AddSettings()
                .AddLanguageService()
                .AddCategoryService()
                .AddOperatorService()
                .AddEmailSender()
                .AddRabbitMq()
                .AddUserAccountService();


        }
    }
}
