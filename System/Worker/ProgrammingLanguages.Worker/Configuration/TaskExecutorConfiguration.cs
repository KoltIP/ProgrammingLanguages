﻿

using ProgrammingLanguages.Worker.TaskExecutors;

namespace ProgrammingLanguages.Worker.Configuration
{
    public static class TaskExecutorConfiguration
    {
        public static IApplicationBuilder StartTaskExecutor(this WebApplication app)
        {
            var executor = app.Services.GetRequiredService<ITaskExecutor>();
            executor.Start();

            return app;
        }
    }
}
