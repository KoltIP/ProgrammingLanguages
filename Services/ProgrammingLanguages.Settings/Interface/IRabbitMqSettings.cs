using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Interface
{
    public interface IRabbitMqSettings
    {
        string Uri { get; }
        string UserName { get; }
        string Password { get; }
    }
}
