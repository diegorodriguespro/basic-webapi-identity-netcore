using System.ComponentModel.DataAnnotations;

namespace web_identity_csharp_base.Models
{
    public class SignInModel
    {
        [EmailAddress]
        [Required(ErrorMessage="Nome de usuário é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage="Password é obrigatório")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
       
    }
}