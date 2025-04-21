
using ApiInventarioCCL.Data;
using ApiInventarioCCL.Models;
using ApiInventarioCCL.Repositories.Irepository;

namespace ApiInventarioCCL.Repositories
{
    public class MoveProductRepository : IMoveProductRepository
    {
        private readonly ApplicationDbContext _db;

        public MoveProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateMoveProduct(MoveProduct move)
        {
            move.date_move = DateTime.UtcNow;
            _db.MoveProducts.Add(move);
            return SaveMoveProduct();
        }

        public bool DeleteMoveProduct(int id)
        {
            _db.MoveProducts.Remove(GetMoveProductById(id));
            return SaveMoveProduct();
        }


        public bool ExistMoveProductById(int id)
        {
            return _db.MoveProducts.Any(p => p.Id == id);
        }


        public ICollection<MoveProduct> GetMoveProduct()
        {
            return _db.MoveProducts.OrderBy(p => p.date_move).ToList();
        }

        public MoveProduct GetMoveProductById(int id)
        {
            return _db.MoveProducts.FirstOrDefault(p => p.Id == id);
        }

        public ICollection<MoveProduct> GetMoveProductByIdProduct(int poductId)
        {
            return _db.MoveProducts.OrderBy(p => p.productId).ToList();
        }

        public bool SaveMoveProduct()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateMoveProduct(MoveProduct move)
        {
            // move.date_move = DateTime.UtcNow;

            var productExist = _db.MoveProducts.Find(move.Id);
            if (productExist != null)
            {
                _db.Entry(productExist).CurrentValues.SetValues(move);
            }

            _db.MoveProducts.Update(move);
            return SaveMoveProduct();
        }
    }
}
