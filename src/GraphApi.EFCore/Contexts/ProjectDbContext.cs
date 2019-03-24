using System;
using System.Collections.Generic;
using System.Linq;
using GraphApi.EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphApi.EFCore.Contexts
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
                new Project[]
                {
                 new Project { Id=1, ProjectName="Alpha", StartingDate = DateTime.Now },
                 new Project { Id=2, ProjectName="Betta", StartingDate= DateTime.Now },
                 new Project { Id=3, ProjectName="Gamma", StartingDate = DateTime.Now }
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
