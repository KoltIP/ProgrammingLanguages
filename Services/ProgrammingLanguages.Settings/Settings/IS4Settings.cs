using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Settings
{
    public class IS4Settings : IIS4Settings
    {
        private readonly ISettingsSource source;
        private readonly IDbSettings dbSettings;

        public IS4Settings(ISettingsSource source) => this.source = source;

        public IS4Settings(ISettingsSource source, IDbSettings dbSettings)
        {
            this.source = source;
            this.dbSettings = dbSettings;
        }

        public IDbSettings Db => dbSettings ?? new DbSettings(source);
    }
}
