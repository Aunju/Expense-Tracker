using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Expense_Tracker.Models
{
    public class SignUp
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress(ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
