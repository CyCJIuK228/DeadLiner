using System.ComponentModel.DataAnnotations;

namespace DeadLinerWebApp.PL.Models
{
    public class RecoveryPasswordViewModel
    {
        public string Email;

        [Required(ErrorMessage = "Password not specified.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }
    }
}