using System.Collections.Generic;

namespace DeadLinerWebApp.PL.Models
{
    public class TaskViewModel
    {
        public string HubName { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public string References { get; set; }
        public List<string> UsersName { get; set; }
        public List<string> AssignedTo { get; set; }
    }
}