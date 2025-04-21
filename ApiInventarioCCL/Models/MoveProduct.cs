using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiInventarioCCL.Models
{
    public class MoveProduct
    {

        [Key]
        public int Id { get; set; }

        public enum MoveType { inside, go_out}
        public MoveType Moveproduct { get; set; }

        [Required]
        [Display(Name = "Fecha_del_movimiento")]
        public DateTime date_move { get; set; }

        // Relacion con tabla product
        public int productId { get; set; }
        [ForeignKey("productId")]
        public Producto Producto { get; set; }

        [Required]
        [Display(Name = "Cantidad_Movida")]
        public int amountMove { get; set; }
    }
}
