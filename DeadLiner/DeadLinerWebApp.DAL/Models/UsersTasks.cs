using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Models
{
    public class UsersTasks
    {
        public int UsersTasksId { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
