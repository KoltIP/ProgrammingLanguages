using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Interface
{
    public interface IEmailSettings
    {
        string FromName { get; }
        string FromEmail { get; }
        string Server { get; }
        int Port { get; }
        string Login { get; }
        string Password { get; }
        bool Ssl { get; }
    }
}
