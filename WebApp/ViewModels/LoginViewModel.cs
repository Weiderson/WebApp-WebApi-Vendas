using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}