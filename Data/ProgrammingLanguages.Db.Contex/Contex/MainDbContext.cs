using ProgrammingLanguages.Db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingLanguages.Db.Context.Context
{
    public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Language> Languages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operator> Operators { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

            modelBuilder.Entity<Language>().ToTable("languages");
            modelBuilder.Entity<Language>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Language>().Property(x => x.Description).HasMaxLength(50);
            modelBuilder.Entity<Language>().HasIndex(x => x.Name).IsUnique();


            modelBuilder.Entity<Operator>().ToTable("operators");
            modelBuilder.Entity<Operator>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Operator>().Property(x => x.Description).HasMaxLength(50);
            modelBuilder.Entity<Operator>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Content).IsRequired();

            modelBuilder.Entity<Language>().HasOne(x => x.Category).WithMany(x => x.Languages).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Operator>().HasOne(x => x.Language).WithMany(x => x.Operators).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>().HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>().HasOne(x => x.Languguage).WithMany(x => x.Comments).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
