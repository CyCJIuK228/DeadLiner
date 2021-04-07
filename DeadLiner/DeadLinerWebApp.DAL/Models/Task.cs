using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Resources { get; set; }
        public List<UsersTasks> UsersTasks{ get; set; }
        public List<User> Users { get; set; }
        public TasksHubs TasksHubs { get; set; }
    }
}