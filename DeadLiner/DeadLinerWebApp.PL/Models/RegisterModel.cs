using System.ComponentModel.DataAnnotations;

namespace DeadLinerWebApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email not specified.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }
    }
}