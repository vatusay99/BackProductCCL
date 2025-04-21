using ApiInventarioCCL.Models;

namespace ApiInventarioCCL.Repositories.Irepository
{
    public interface IProductRepository
    {
        ICollection<Producto> GetProducs();

        Producto GetProductById(int id);

        bool ExistProductById(int id);

        bool ExistProductByName(string name);

        IEnumerable<Producto> GetByName(string name);
        bool CreateProduct(Producto p);

        bool UpdateProduct(Producto p);

        bool DeleteProduct(int id);

        bool Save();

    }
}
