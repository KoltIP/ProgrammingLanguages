using Microsoft.Extensions.DependencyInjection;
using ProgrammingLanguages.Api.Configuration;
using ProgrammingLanguages.Api.Test.Common;
using ProgrammingLanguages.Db.Context.Setup;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;
using Serilog;
using System;

namespace ProgrammingLanguages.Api.Test.TestService
{
    public class TestServices
    {
        private readonly IServiceCollection services = new ServiceCollection();
        public IServiceProvider ServiceProvider { get; set; }

        public TestServices()
        {
            var configuration = ConfigurationFactory.GetApiConfiguration();

            // Logger
            var logger = new LoggerConfiguration()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var settings = new ApiSettings(new SettingsSource(configuration));

            services.AddHttpContextAccessor();

            services.AddAppDbContext(settings);

            services.AddAppVersions();

            services.AddAppServices();

            services.AddAutoMappers();

            services.AddControllers().AddValidator();

            ServiceProvider = services.BuildServiceProvider();

            DbInit.Execute(ServiceProvider);

            //DbSeed.Execute(ServiceProvider);
        }

        public T Get<T>()
        {
            return ServiceProvider.GetService<T>();
        }


    }
}
