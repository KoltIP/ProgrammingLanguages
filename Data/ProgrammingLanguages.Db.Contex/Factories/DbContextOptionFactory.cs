using ProgrammingLanguages.Db.Context.Context;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingLanguages.Db.Context.Factories
{
    public class DbContextOptionFactory
    {
        public static DbContextOptions<MainDbContext> Create(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            Configure(connectionString).Invoke(builder);
            return builder.Options;
        }

        public static Action<DbContextOptionsBuilder> Configure (string connectionString)
        {
            return (builder) => builder.UseNpgsql (connectionString, opt =>
            {
                opt.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
            });
        }
    }
}