using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Context.Factories;
using ProgrammingLanguages.Settings.Interface;

namespace ProgrammingLanguages.Identity.Configuration
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IDbSettings settings)
        {
            var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.ConnectionString);

            services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

            return services;
        }

        public static IApplicationBuilder UseAppDbContext(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
