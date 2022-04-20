using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.UserAccount
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddUserAccountService(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountService, UserAccountService>();  // !!!  Обратите внимание, что UserAccount должен объявляться как SCOPED

            return services;
        }
    }
}
