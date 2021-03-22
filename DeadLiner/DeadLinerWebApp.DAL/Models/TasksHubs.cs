using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Models
{
    public class TasksHubs
    {
        public int TasksHubsId { get; set; }
        public int HubId { get; set; }
        public Hub Hub { get; set; }
        public List<Task> Tasks{ get; set; }
    }
}
