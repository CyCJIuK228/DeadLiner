using System.Collections.Generic;

namespace DeadLinerWebApp.PL.Models
{
    public class UsersHubsViewModel
    {
        public string Role { get; set; }
        public string MentorName { get; set; }
        public List<Assignment> Assignments { get; set; }
    }

    public class Assignment
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public AssignmentStatus Status { get; set; }
    }

    public enum AssignmentStatus
    {
        New,
        InProgress,
        Closed
    }
}