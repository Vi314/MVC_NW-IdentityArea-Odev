using System.ComponentModel.DataAnnotations;

namespace Northwind_MVC.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="Username field is required!")]
        public string Username { get; set; }
        
        
        [Required(ErrorMessage = "Email field is required!")]
        [EmailAddress(ErrorMessage ="Email format is incorrect")]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Password field is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "PasswordRepeat field is required!")]
        [Compare("Password",ErrorMessage ="Passwords do not match!")]
        public string PasswordRepeat { get; set; }

    }
}
