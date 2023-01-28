using System.ComponentModel.DataAnnotations;

namespace Northwind_MVC.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username field is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password field is required!")]
        public string Password { get; set; }
    }
}
