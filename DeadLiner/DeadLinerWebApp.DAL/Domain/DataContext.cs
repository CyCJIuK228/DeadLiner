using DeadLinerWebApp.DAL.Helper;
using DeadLinerWebApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadLinerWebApp.DAL.Domain
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<UsersHubs> UsersHubs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
            modelBuilder.ConfigureData();
            base.OnModelCreating(modelBuilder);
        }
    }
}