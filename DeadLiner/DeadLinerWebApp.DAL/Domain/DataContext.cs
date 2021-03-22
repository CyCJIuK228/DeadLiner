using DeadLinerWebApp.DAL.Helper;
using DeadLinerWebApp.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeadLinerWebApp.DAL.Domain
{
     
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DataContext() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UsersHubs> UsersHubs { get; set; }
        public DbSet<UsersTasks> UsersTasks { get; set; }
        public DbSet<TasksHubs> TasksHubs { get; set; }
        public DbSet<Invites> Invites { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.SeedData();
            modelBuilder.ConfigureData();

            base.OnModelCreating(modelBuilder);
        }
    }
}
