using System.ComponentModel.DataAnnotations;

namespace VidlyApp.View_Model
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password doesnt Match")]
        public string ConfirmPassword { get; set; }
    }
}
