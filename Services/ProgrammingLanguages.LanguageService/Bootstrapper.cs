using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.LanguageService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddLanguageService(this IServiceCollection services)
        {
            services.AddSingleton<ILanguageService, LanguageService>();
            

            return services;
        }
    }
}
