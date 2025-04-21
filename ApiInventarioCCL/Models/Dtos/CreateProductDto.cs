using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "El nombre es campo obligatorio")]
        public string name { get; set; }


        [Required(ErrorMessage = "La cantidad es campo obligatorio")]
        public int amount { get; set; }
    }
}
