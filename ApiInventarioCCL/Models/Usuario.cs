using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiInventarioCCL.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
