using System.ComponentModel.DataAnnotations;

namespace DeadLinerWebApp.PL.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email not specified.")]
        [EmailAddress(ErrorMessage = "Email is in the incorrect format.")]
        public string Email { get; set; }
    }
}