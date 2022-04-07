using ProgrammingLanguages.CategoryService;
using ProgrammingLanguages.LanguageService;
using ProgrammingLanguages.OperatorService;
using ProgrammingLanguages.Settings;

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
                .AddOperatorService();
                
        }
    }
}
