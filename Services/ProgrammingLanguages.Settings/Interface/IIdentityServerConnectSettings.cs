using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Interface
{
    public interface IIdentityServerConnectSettings
    {
        string Url { get; }
        string ClientId { get; }
        string ClientSecret { get; }
        bool RequireHttps { get; }
    }
}
