using DeadLinerWebApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Helper
{
    public static class ConfiguringDataExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var roleList = new List<Role>
            {
                new Role {RoleId = 1, Title = "mentee"},
                new Role {RoleId = 2, Title = "mentor"}
            };

            var userList = new List<User>
            {
                new User {UserId = 1, FullName = "TestFullName1", Email = "testEmail@lnu.edu.ua", Password = "test"},
                new User {UserId = 2, FullName = "Serhii Yurko", Email = "Serhii.Yurko@lnu.edu.ua", Password = "serhii"}
            };

            var hubList = new List<Hub>
            {
                new Hub {HubId = 1, Code = "testCodeHub", Name = "testNameHub"}
            };

            var usersHubs = new List<UsersHubs>
            {
                new UsersHubs {UsersHubsId = 1, HubId = 1, RoleId = 1, UserId = 1, TeamName = "testTeam1"},
                new UsersHubs {UsersHubsId = 2, HubId = 1, RoleId = 1, UserId = 2, TeamName = "testTeam1"}
            };

            var TaskList = new List<Task>
            {
                new Task {TaskId = 1, Name = "TODO Index page", Description = "Make a good visual concept", Resources = "https://www.w3schools.com"},
                new Task {TaskId = 2, Name = "TODO Home page", Description = "Make a good visual concept", Resources = "https://www.w3schools.com"}
            };



            modelBuilder.Entity<Role>().HasData(roleList);
            modelBuilder.Entity<User>().HasData(userList);
            modelBuilder.Entity<Hub>().HasData(hubList);
            modelBuilder.Entity<UsersHubs>().HasData(usersHubs);
            modelBuilder.Entity<Task>().HasData(TaskList);
        }

        public static void ConfigureData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hub>().HasMany(c => c.Users).WithMany(s => s.Hubs)
                .UsingEntity<UsersHubs>(
                    j => j.HasOne(pt => pt.User).WithMany(t => t.UsersHubs).HasForeignKey(k => k.UserId),
                    j => j.HasOne(t => t.Hub).WithMany(t => t.UsersHubs)
                        .HasForeignKey(k => k.HubId),
                    j =>
                    {
                        j.HasKey(t => new { t.UserId, t.HubId });
                    }
                );
            modelBuilder.Entity<Hub>().HasMany(c => c.Users).WithMany(s => s.Hubs)
               .UsingEntity<Invites>(
                   j => j.HasOne(pt => pt.User).WithMany(t => t.Invites).HasForeignKey(k => k.UserId),
                   j => j.HasOne(t => t.Hub).WithMany(t => t.Invites)
                       .HasForeignKey(k => k.HubId),
                   j =>
                   {
                       j.HasKey(t => new { t.UserId, t.HubId, t.Status });
                   }
               );

            modelBuilder.Entity<Task>().HasMany(c => c.Users).WithMany(s => s.Tasks)
               .UsingEntity<UsersTasks>(
                   j => j.HasOne(pt => pt.User).WithMany(t => t.UsersTasks).HasForeignKey(k => k.UserId),
                   j => j.HasOne(t => t.Task).WithMany(t => t.UsersTasks)
                       .HasForeignKey(k => k.TaskId),
                   j =>
                   {
                       j.HasKey(t => new { t.UserId, t.TaskId });
                   }
               );
        }
    }
}
