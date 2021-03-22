using System.ComponentModel.DataAnnotations;

namespace DeadLinerWebApp.PL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email not specified.")]
        [EmailAddress(ErrorMessage = "Email is in the incorrect format.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password not specified.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}