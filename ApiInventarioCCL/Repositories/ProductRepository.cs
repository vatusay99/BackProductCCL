
using ApiInventarioCCL.Data;
using ApiInventarioCCL.Models;
using ApiInventarioCCL.Repositories.Irepository;

namespace ApiInventarioCCL.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateProduct(Producto p)
        {
            p.creation_date = DateTime.UtcNow;
            _db.Productos.Add(p);
            return Save();
        }

        public bool DeleteProduct(int id)
        {
            _db.Productos.Remove(GetProductById(id));
            return Save();
        }

        public bool ExistProductById(int id)
        {
            return _db.Productos.Any(p => p.Id == id);
        }

        public bool ExistProductByName(string name)
        {
            bool valor = _db.Productos.Any(p => p.name.ToLower().Trim() == name.ToLower().Trim());
            return valor;
        }

        public IEnumerable<Producto> GetByName(string name) 
        {
            IQueryable<Producto> query = _db.Productos;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.name.Contains(name));
            }

            return query;
        }

        public ICollection<Producto> GetProducs()
        {
            return _db.Productos.OrderBy(p => p.name).ToList();
        }

        public Producto GetProductById(int id)
        {
            /*if (!ExistProductById(id)) 
            {
                throw new Exception("El id no fue encontrado.");
            }*/

            return _db.Productos.FirstOrDefault(p => p.Id == id);
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateProduct(Producto p)
        {
            p.creation_date = DateTime.UtcNow;

            var productExist = _db.Productos.Find(p.Id);
            if (productExist != null)
            {
                _db.Entry(productExist).CurrentValues.SetValues(p);
            }
            else 
            {
                _db.Productos.Update(p);
            }

            return Save();
        }
    }
}
