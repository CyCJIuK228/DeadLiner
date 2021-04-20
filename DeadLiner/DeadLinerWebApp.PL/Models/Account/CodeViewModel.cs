using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DeadLinerWebApp.PL.Models
{
    public class CodeViewModel
    {
        [Required(ErrorMessage = "Code not specified.")]
        [NotNull]
        public string ActualCode { get; set; }
    }
}