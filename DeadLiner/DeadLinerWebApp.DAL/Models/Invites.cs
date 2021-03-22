using System.Collections.Generic;

namespace DeadLinerWebApp.DAL.Models
{
    public class Invites
    {
        public int InvitesId { get; set; }
        public int HubId { get; set; }
        public Hub Hub { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
    }
}
