using System.ComponentModel.DataAnnotations;

namespace DeadLinerWebApp.PL.Models
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email not specified.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string ConfirmPassword { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Please accept terms.")]
        public bool IsAcceptedTerms { get; set; }
    }
}