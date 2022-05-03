using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.Db.Entities
{
    public class Language:BaseEntity
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public virtual Category Category { get; set; }
        public virtual ICollection<Operator> Operators { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
