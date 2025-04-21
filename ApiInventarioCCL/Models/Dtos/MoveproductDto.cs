using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models.Dtos
{
    public class MoveproductDto
    {
        public int Id { get; set; }

        public enum MoveType { inside, go_out }

        [Required]
        public MoveType Moveproduct { get; set; }

        [Required]
        public DateTime date_move { get; set; }

        public int productId { get; set; }


        [Required]
        public int amountMove { get; set; }
    }
}
