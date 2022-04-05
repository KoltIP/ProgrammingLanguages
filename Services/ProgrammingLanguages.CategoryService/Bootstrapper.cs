using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CategoryService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddCategoryService(this IServiceCollection services)
        {
            services.AddSingleton<ICategoryService, CategoryService>();


            return services;
        }
    }
}
