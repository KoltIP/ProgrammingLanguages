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


            var a1 = new Entities.Language()
            {
                Id = 1,
                Name = "C#",
                Description = "1",
                CategoryId =1,
            };
            context.Languages.Add(a1);
            var b1 = new Entities.Category()
            {
                Name = "Объекто-ориентированные",
                Id = 1,
            };
            context.Categories.Add(b1);
            var c1 = new Entities.Operator()
            {
                Id = 1,
                LanguageId = 1,
                Name = "Plus",
                Description = "...",
            };
            context.Operators.Add(c1);

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