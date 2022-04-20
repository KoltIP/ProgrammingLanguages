using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Interface
{
    public interface IWorkerSettings
    {
        IDbSettings Db { get; }
        IRabbitMqSettings RabbitMq { get; }
        IEmailSettings Email { get; }
    }
}
