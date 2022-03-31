using ProgrammingLanguages.LanguageService;
using ProgrammingLanguages.Settings;

namespace ProgrammingLanguages.Api
{
    public static class Bootstrapper
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services
                .AddSettings()
                .AddLanguageService();
        }
    }
}
