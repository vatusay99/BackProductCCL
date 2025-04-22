using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es un campo obligatorio")]
        [MaxLength(15)]
        public string Password { get; set; }
    }
}
