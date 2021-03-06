namespace DeadLinerWebApp.DAL.Entity
{
    public class UsersTasks
    {
        public int UsersTasksId { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }

    public enum TaskStatus
    {
        New,
        InProgress,
        Closed,
    }
}