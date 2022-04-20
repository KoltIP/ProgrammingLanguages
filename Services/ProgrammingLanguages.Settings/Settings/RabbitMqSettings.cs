using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Settings
{
    public class RabbitMqSettings: IRabbitMqSettings
    {
        private readonly ISettingsSource source;

        public RabbitMqSettings(ISettingsSource source)
        {
            this.source = source;
        }

        public string Uri => source.GetAsString("RabbitMQ:Uri");
        public string UserName => source.GetAsString("RabbitMQ:Username");
        public string Password => source.GetAsString("RabbitMQ:Password");
    }
}
