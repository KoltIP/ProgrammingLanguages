using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CommentService
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddCommentService(this IServiceCollection services)
        {
            services.AddSingleton<ICommentService, CommentService>();


            return services;
        }
    }
}
