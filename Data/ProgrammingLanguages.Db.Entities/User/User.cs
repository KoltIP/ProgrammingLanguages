using Microsoft.AspNetCore.Identity;

namespace ProgrammingLanguages.Db.Entities
{

    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public UserStatus Status { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        ///??
        public virtual ICollection<Subscription> Subscriptions { get; set; }
       
    }
}