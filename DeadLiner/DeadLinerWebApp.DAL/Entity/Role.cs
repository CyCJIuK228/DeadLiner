using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Entity
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public List<UsersHubs> UsersHubs { get; set; }
    }
}