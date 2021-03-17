using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Hub> Hubs { get; set; }
        public List<UsersHubs> UsersHubs { get; set; }
    }
}