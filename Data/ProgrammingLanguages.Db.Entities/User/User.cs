using Microsoft.AspNetCore.Identity;

namespace ProgrammingLanguages.Db.Entities
{

    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public UserStatus Status { get; set; }
    }
}