using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.OperatorService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddOperatorService(this IServiceCollection services)
        {
            services.AddSingleton<IOperatorService, OperatorService>();


            return services;
        }
    }
}
