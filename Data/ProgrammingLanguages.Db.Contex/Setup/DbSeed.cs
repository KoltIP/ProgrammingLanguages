using ProgrammingLanguages.Db.Context.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProgrammingLanguages.Db.Context.Setup
{

    public static class DbSeed
    {
        private static void AddLanguage(MainDbContext context)
        {
            if (context.Languages.Any() || context.Categories.Any() || context.Operators.Any())
                return;
                        
            var c1 = new Entities.Category()
            {
                Name = "Объекто-ориентированные",
                Description= "1",
                Id = 1,
            };
            context.Categories.Add(c1);

            var l1 = new Entities.Language()
            {
                Id = 1,
                Name = "C#",
                Description = "1",
                CategoryId = 1,
            };
            context.Languages.Add(l1);

            
            var o1 = new Entities.Operator()
            {
                Id = 1,
                LanguageId = 1,
                Name = "Plus",
                Description = "...",
            };
            context.Operators.Add(o1);

            context.SaveChanges();
        }

        public static void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
            ArgumentNullException.ThrowIfNull(scope);

            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
            using var context = factory.CreateDbContext();

            AddLanguage(context);
        }
    }
}