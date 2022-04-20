using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ProgrammingLanguages.EmailService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();

            return services;
        }
    }
}
