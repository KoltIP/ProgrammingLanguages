using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Db.Entities
{
    public class Subscription : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Languguage { get; set; }
    }
}
