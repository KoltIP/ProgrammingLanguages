using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Context.Factories;
using ProgrammingLanguages.Db.Context.Setup;
using ProgrammingLanguages.Settings.Interface;

namespace ProgrammingLanguages.Api.Configuration
{
    public static class DbConfigurationcs
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IApiSettings settings)
        {
            var dbOptionsDelegate = DbContextOptionFactory.Configure(settings.Db.ConnectionString);

            services.AddDbContextFactory<MainDbContext>(dbOptionsDelegate, ServiceLifetime.Singleton);

            return services;
        }

        public static IApplicationBuilder UseAppDbContext(this IApplicationBuilder app)
        {
            DbInit.Execute(app.ApplicationServices);

            DbSeed.Execute(app.ApplicationServices);

            return app;
        }
    }
}
