using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ProgrammingLanguages.RabbitMqService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMq, RabbitMq>();
            services.AddSingleton<IRabbitMqTask, RabbitMqTask>();

            return services;
        }
    }
}
