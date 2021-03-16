using System.Collections.Generic;
using DeadLinerWebApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadLinerWebApp.DAL.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userList = new List<User>
            {
                new User {Id = 1, FullName = "Serhii Yurko", Email = "Serhii.Yurko@lnu.edu.ua"}
            };

            modelBuilder.Entity<User>().HasData(userList);
            base.OnModelCreating(modelBuilder);
        }
    }
}