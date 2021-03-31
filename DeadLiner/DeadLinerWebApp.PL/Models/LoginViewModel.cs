using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DeadLinerWebApp.PL.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email not specified.")]
        [EmailAddress(ErrorMessage = "Email is in the incorrect format.")]
        [NotNull]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified.")]
        [DataType(DataType.Password)]
        [NotNull]
        public string Password { get; set; }

        public bool IsRemember { get; set; }
    }
}