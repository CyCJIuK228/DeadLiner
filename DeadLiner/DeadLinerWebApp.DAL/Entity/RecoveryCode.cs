namespace DeadLinerWebApp.DAL.Entity
{
    public class RecoveryCode 
    {
        public int RecoveryCodeId { get; set; }
        public string Code { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}