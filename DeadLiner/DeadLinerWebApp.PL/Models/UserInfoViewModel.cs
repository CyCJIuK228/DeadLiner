using System;

namespace DeadLinerWebApp.PL.Models
{
    public class UserInfoViewModel
    {
        public string Email { get; set; }
        public string University { get; set; }
        public string Group { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDay { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}