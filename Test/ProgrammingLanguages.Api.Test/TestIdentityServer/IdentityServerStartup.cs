﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgrammingLanguages.Identity.Configuration;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Settings.Settings;

namespace ProgrammingLanguages.Api.Test.TestIdentityServer
{
    public class IdentityServerStartup
    {
        private readonly IConfiguration configuration;
        private readonly Action<IServiceCollection>? configurator;

        public IdentityServerStartup(
            IConfiguration configuration, Action<IServiceCollection>? configurator = null
        )
        {
            this.configuration = configuration;
            this.configurator = configurator;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new IS4Settings(new SettingsSource(configuration));

            services.AddAppCors();
            services.AddAppDbContext(settings.Db);
            services.AddRouting();
            services.AddHttpContextAccessor();
            services.AddAppServices();
            services.AddIS4();

            configurator?.Invoke(services); // what is it? for what?
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAppCors();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAppDbContext();
            app.UseIS4();
        }

    }
}
