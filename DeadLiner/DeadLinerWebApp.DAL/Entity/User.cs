using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RecoveryCode> Code { get; set; }
        public List<Hub> Hubs { get; set; }
        public List<UsersHubs> UsersHubs { get; set; }
        public List<Task> Tasks { get; set; }
        public List<UsersTasks> UsersTasks { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}