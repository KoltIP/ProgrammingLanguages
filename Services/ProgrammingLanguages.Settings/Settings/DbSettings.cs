using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Settings
{
    public class DbSettings : IDbSettings
    {
        private readonly ISettingsSource source;
        public DbSettings(ISettingsSource source) => this.source = source;

        public string ConnectionString => source.GetConnectionString("MainDbContext");
    }
}
