using ApiInventarioCCL.Models;

namespace ApiInventarioCCL.Repositories.Irepository
{
    public interface IMoveProductRepository
    {
        ICollection<MoveProduct> GetMoveProduct();
        ICollection<MoveProduct> GetMoveProductByIdProduct(int poductId);

        MoveProduct GetMoveProductById(int id);

        bool ExistMoveProductById(int id);

        bool CreateMoveProduct(MoveProduct move);

        bool UpdateMoveProduct(MoveProduct move);

        bool DeleteMoveProduct(int id);

        bool SaveMoveProduct();

    }
}
