namespace DeadLinerWebApp.DAL.Entity
{
    public class UsersHubs
    {
        public int UsersHubsId { get; set; }
        public int HubId { get; set; }
        public Hub Hub { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string TeamName { get; set; }
    }
}