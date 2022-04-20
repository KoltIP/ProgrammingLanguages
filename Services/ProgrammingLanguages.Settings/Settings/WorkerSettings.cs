using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Settings
{
    public class WorkerSettings : IWorkerSettings
    {
        private readonly ISettingsSource source;
        private readonly IDbSettings dbSettings;
        private readonly IRabbitMqSettings rabbitMqSettings;
        private readonly IEmailSettings emailSettings;

        public WorkerSettings(ISettingsSource source) => this.source = source;

        public WorkerSettings(ISettingsSource source, IDbSettings dbSettings, IRabbitMqSettings rabbitMqSettings, IEmailSettings emailSettings)
        {
            this.source = source;
            this.dbSettings = dbSettings;
            this.rabbitMqSettings = rabbitMqSettings;
            this.emailSettings = emailSettings;
        }

        public IDbSettings Db => dbSettings ?? new DbSettings(source);
        public IRabbitMqSettings RabbitMq => rabbitMqSettings ?? new RabbitMqSettings(source);
        public IEmailSettings Email => emailSettings ?? new EmailSettings(source);
    }
}
