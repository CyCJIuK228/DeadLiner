using System;

namespace DeadLinerWebApp.DAL.Entity
{
    public class UserInfo
    {
        public int UserInfoId { get; set; }
        public string University { get; set; }
        public string Group { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDay { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}