using System.ComponentModel.DataAnnotations;

namespace web_identity_csharp_base.Models
{
    public class SignUpModel
    {
        [Required]
        [EmailAddress]
        //[DataType(DataType.EmailAddress, ErrorMessage="Endereço de email ou senha inválidos")] // utilizado para validar no Razor, no cliente
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
    }
}