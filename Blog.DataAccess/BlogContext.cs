using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;


//Imena za diplomski blog : warlog,battlog
namespace Blog.DataAccess
{
    public class BlogContext : DbContext
    {
        // otkomentarisati pri migracijama
        //public BlogContext()
        //{

        //}
        public BlogContext(DbContextOptions options = null) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
        // ukoliko trazi neku referencu za api assembly, restartovati VisualStudio i popravice se
        // Pri migracijjama staviti da se pali DataAccess Sloj

        // otkomentarisati pri migracijama
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //    optionsbuilder.UseSqlServer(@"Data Source=.\sqlexpress;Initial Catalog=blograd;Integrated Security=true");
        //    base.OnConfiguring(optionsbuilder);
        //}

        // Azure baza
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        //{
        //    optionsbuilder.UseSqlServer(@"Server=tcp:blograd.database.windows.net,1433;Initial Catalog=blograd;Persist Security Info=False;User ID=blogradadmin;Password=lozinka123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //    base.OnConfiguring(optionsbuilder);
        //}
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<VoteType> VoteTypes { get; set; }
        public DbSet<UseCase> UseCases { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostCategory> BlogPostCategories { get; set; }
        public DbSet<BlogPostImage> BlogPostImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }

    }
}
