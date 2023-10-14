using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Auth.Model.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
