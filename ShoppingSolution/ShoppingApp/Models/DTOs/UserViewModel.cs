using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Models.DTOs
{
    public class UserViewModel : UserDTO
    {
        [Required(ErrorMessage = "ReType password cannot be empty")]
        [Compare("Password", ErrorMessage = "Password and ReType password do not match")]
        public string ReTypePassword { get; set; }
    }
}