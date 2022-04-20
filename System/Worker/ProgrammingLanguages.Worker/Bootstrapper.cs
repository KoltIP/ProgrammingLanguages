using ProgrammingLanguages.EmailService;
using ProgrammingLanguages.RabbitMqService;
using ProgrammingLanguages.Settings;
using ProgrammingLanguages.Worker.TaskExecutors;

namespace ProgrammingLanguages.Worker
{
    public static class Bootstrapper
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddSettings()
                .AddEmailSender()
                .AddRabbitMq()
                .AddSingleton<ITaskExecutor, TaskExecutor>();

            return services;
        }
    }
}
