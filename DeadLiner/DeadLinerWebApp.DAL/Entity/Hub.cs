using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Entity
{
    public class Hub
    {
        public int HubId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<User> Users { get; set; }
        public List<Task> Tasks { get; set; }
        public List<UsersHubs> UsersHubs { get; set; }
    }
}