using System.ComponentModel.DataAnnotations;

namespace ApiInventarioCCL.Models.Dtos
{
    public class CreateMoveproductDto
    {
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
