using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ProgrammingLanguages.Api.Test.Common.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task Truncate<T>(this DbContext context, DbSet<T> dbSet) where T : class
        {
            await context.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {dbSet.EntityType.GetTableName()}");
        }
    }
}
