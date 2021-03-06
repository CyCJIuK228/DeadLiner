using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DeadLinerWebApp.DAL.Entity;

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
                new Hub {HubId = 1, Code = "testCodeHub1", Name = "testNameHub1", Description = "testHubDescription1"},
                new Hub {HubId = 2, Code = "testCodeHub2", Name = "testNameHub2", Description = "testHubDescription2"}
            };

            var usersHubs = new List<UsersHubs>
            {
                new UsersHubs {UsersHubsId = 1, HubId = 1, RoleId = 1, UserId = 1, TeamName = "testTeam1"},
                new UsersHubs {UsersHubsId = 2, HubId = 1, RoleId = 1, UserId = 2, TeamName = "testTeam1"}
            };

            var taskList = new List<Task>
            {
                new Task {TaskId = 1, Name = "TODO Index page", Description = "Make a good visual concept", Resources = "https://www.w3schools.com", HubId = 1},
                new Task {TaskId = 2, Name = "TODO Home page", Description = "Make a good visual concept", Resources = "https://www.w3schools.com", HubId = 1}
            };

            var codes = new List<RecoveryCode>
            {
                new RecoveryCode {RecoveryCodeId = 1, Code = "test", UserId = 1}
            };

            var usersTasks = new List<UsersTasks>
            {
                new UsersTasks {UsersTasksId = 1, TaskId = 1, TaskStatus = TaskStatus.New, UserId = 1},
                new UsersTasks {UsersTasksId = 1, TaskId = 2, TaskStatus = TaskStatus.InProgress, UserId = 1},
            };

            var invites = new List<Invite>
            {
                new Invite {InviteId = 1, HubId = 1, Status = "new", UserId = 1}
            };

            modelBuilder.Entity<Role>().HasData(roleList);
            modelBuilder.Entity<User>().HasData(userList);
            modelBuilder.Entity<Hub>().HasData(hubList);
            modelBuilder.Entity<Hub>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<UsersHubs>().HasData(usersHubs);
            modelBuilder.Entity<Task>().HasData(taskList);
            modelBuilder.Entity<RecoveryCode>().HasData(codes);
            modelBuilder.Entity<UsersTasks>().HasData(usersTasks);
            modelBuilder.Entity<UsersTasks>().Property(p => p.TaskStatus).HasConversion<string>();
            modelBuilder.Entity<Invite>().HasData(invites);
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