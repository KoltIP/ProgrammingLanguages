using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Db.Entities
{
    public class Operator:BaseEntity
    {
        public int? LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public virtual Language Language { get; set; }
    }
}
