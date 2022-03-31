using ProgrammingLanguages.Settings.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Settings.Settings
{
    public class GeneralSettings : IGeneralSettings
    {
        private readonly ISettingsSource source;
        public GeneralSettings(ISettingsSource source) => this.source = source;

        public bool SwaggerVisible => source.GetAsBool("General:SwaggerVisible");
    }
}
