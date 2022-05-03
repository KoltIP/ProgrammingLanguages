using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using ProgrammingLanguages.Api.Configuration;
using ProgrammingLanguages.Db.Context.Setup;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Api.Test.TestApiServer
{
    public class ApiStartup
    {
        private readonly IConfiguration configuration;
        private readonly Action<IServiceCollection>? configurator;

        public ApiStartup(
            IConfiguration configuration, Action<IServiceCollection>? configurator = null
        )
        {
            this.configuration = configuration;
            this.configurator = configurator;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Logger
            var logger = new LoggerConfiguration()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var settings = new ApiSettings(new SettingsSource(configuration));

            services.AddHttpContextAccessor();

            
            services.AddAppDbContext(settings);

            //services.AddAppHealthCheck();

            services.AddAppVersions();

            services.AddAppSwagger(settings);

            services.AddAppCors();

            services.AddAppServices();

            services.AddAppAuth(settings);

            services.AddControllers().AddValidator();

            services.AddRazorPages();

            services.AddAutoMappers();

            IdentityModelEventSource.ShowPII = true;

            configurator?.Invoke(services); // what is it? for what?
        }

        public void Configure(IApplicationBuilder app)
        {
            Log.Information("Start");

            app.UseMiddlewares();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAppCors();

            app.UseSerilogRequestLogging();

            app.UseAppAuth();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseAppDbContext();
            //DbInit.Execute(app.ApplicationServices);
        }
    }
}
