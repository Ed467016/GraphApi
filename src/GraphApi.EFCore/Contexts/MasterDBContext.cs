using GraphApi.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphApi.EFCore.Contexts
{
    public class MasterDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User[]
            {
                new User { UserId=1, FirstName="Tom", Age=23, ProjectId = 1},
                new User { UserId=2, FirstName="Alice", Age=26, ProjectId = 2},
                new User { UserId=3, FirstName="Sam", Age=28, ProjectId = 3}
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
