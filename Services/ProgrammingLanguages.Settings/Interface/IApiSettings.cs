using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Interface
{
    public interface IApiSettings
    {
        IGeneralSettings General { get; }
        IDbSettings Db { get; }
        IIdentityServerConnectSettings IdentityServer { get; }
    }
}
