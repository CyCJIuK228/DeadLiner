namespace DeadLinerWebApp.DAL.Entity
{
    public class Invite
    {
        public int InviteId { get; set; }
        public int HubId { get; set; }
        public Hub Hub { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Status { get; set; }
    }
}