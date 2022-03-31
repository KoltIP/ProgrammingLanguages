using ProgrammingLanguages.Db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingLanguages.Db.Context.Context
{
    public class MainDbContext : DbContext
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operator> Operators { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>().ToTable("languages");
            modelBuilder.Entity<Language>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Language>().Property(x => x.Description).HasMaxLength(50);
            modelBuilder.Entity<Language>().HasIndex(x => x.Name).IsUnique();


            modelBuilder.Entity<Operator>().ToTable("operator");
            modelBuilder.Entity<Operator>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Operator>().Property(x => x.Description).HasMaxLength(50);
            modelBuilder.Entity<Operator>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Category>().ToTable("category");
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Language>().HasMany(x => x.Categories).WithMany(x => x.Languages).UsingEntity(t => t.ToTable("language_categories"));
            modelBuilder.Entity<Operator>().HasOne(x => x.Language).WithMany(x => x.Operators).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
