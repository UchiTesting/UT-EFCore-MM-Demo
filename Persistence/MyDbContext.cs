using EF_MM.Models;

using Microsoft.EntityFrameworkCore;

namespace EF_MM.Persistence
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            string connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=ManyToManyDemo;Trusted_Connection=True;MultipleActiveResultSets=true";
            if (optionsBuilder is not null)
                optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Student>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEWID()");
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
