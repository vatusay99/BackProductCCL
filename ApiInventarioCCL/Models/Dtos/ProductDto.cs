using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models.Dtos
{
    public class ProductDto
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es campo obligatorio")]
        public string name { get; set; }

        [Required(ErrorMessage = "La cantidad es campo obligatorio")]
        public int amount { get; set; }

        [Display(Name = "Fecha de Ceación")]
        public DateTime creation_date { get; set; }
    }
}
