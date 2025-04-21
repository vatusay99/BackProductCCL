using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }

        [Required]
        public int amount { get; set; }

        [Required]
        [Display(Name = "Fecha de Ceación")]
        public DateTime creation_date { get; set; }

    }
}
