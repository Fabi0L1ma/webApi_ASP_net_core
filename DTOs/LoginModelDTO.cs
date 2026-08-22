using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "Nome do usuario é obrigatorio.")]
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "Senha do usuario é obrigatoria.")]
        public string? Senha { get; set; }
    }
}
