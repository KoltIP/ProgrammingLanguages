using ProgrammingLanguages.Settings;

namespace ProgrammingLanguages.Identity
{
    public static class Bootstrapper
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSettings();
                
        }
    }
}
