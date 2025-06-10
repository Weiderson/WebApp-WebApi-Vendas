using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha obrigatória.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Senha é no mínimo 8 caracteres")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Senha não confere.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirme a senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repetir senha")]
        public string ConfirmPassword { get; set; }
    }
}